using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using cheat_engine.Interfaces;

namespace cheat_engine.Services
{
    /// <summary>
    /// Bellek tarama iþlemlerinin implementasyonu
    /// </summary>
    public class MemoryScanner : IMemoryScanner
    {
        private readonly IProcessManager _processManager;
        private readonly IValueConverter _valueConverter;
        private readonly ILogger _logger;

        // P/Invoke declarations
        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, [Out] byte[] lpBuffer, int dwSize, out IntPtr lpNumberOfBytesRead);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern UIntPtr VirtualQueryEx(IntPtr hProcess, IntPtr lpAddress, out MEMORY_BASIC_INFORMATION lpBuffer, UIntPtr dwLength);

        [StructLayout(LayoutKind.Sequential)]
        private struct MEMORY_BASIC_INFORMATION
        {
            public IntPtr BaseAddress;
            public IntPtr AllocationBase;
            public uint AllocationProtect;
            public IntPtr RegionSize;
            public uint State;
            public uint Protect;
            public uint Type;
        }

        private const uint MEM_COMMIT = 0x1000;
        private const uint PAGE_NOACCESS = 0x01;
        private const uint PAGE_GUARD = 0x100;
        private const uint PAGE_READWRITE = 0x04;
        private const uint PAGE_WRITECOPY = 0x08;
        private const uint PAGE_EXECUTE_READWRITE = 0x40;
        private const uint PAGE_EXECUTE_WRITECOPY = 0x80;

        public MemoryScanner(IProcessManager processManager, IValueConverter valueConverter, ILogger logger)
        {
            _processManager = processManager;
            _valueConverter = valueConverter;
            _logger = logger;
        }

        public List<FoundAddress> ScanMemory(Process process, List<(string subtype, byte[] bytes)> targets, IProgress<int> progress = null)
        {
            var foundAddresses = new List<FoundAddress>();

            try
            {
                IntPtr hProcess = _processManager.OpenProcessForRead(process.Id);
                if (hProcess == IntPtr.Zero)
                {
                    _logger?.SetStatus($"{process.ProcessName}: açýlmadý");
                    _logger?.Log("OpenProcess returned zero.");
                    return foundAddresses;
                }

                try
                {
                    int maxTlen = targets.Max(t => t.bytes?.Length ?? 0);
                    if (maxTlen <= 0)
                    {
                        _logger?.SetStatus("Aranacak hedef boþ");
                        return foundAddresses;
                    }

                    int overlap = Math.Max(0, maxTlen - 1);
                    int structSize = Marshal.SizeOf(typeof(MEMORY_BASIC_INFORMATION));

                    // Ýlk geçiþ: toplam taranacak bellek boyutunu hesapla
                    var regionsToScan = GetWritableRegions(hProcess, structSize);
                    long totalMemoryToScan = regionsToScan.Sum(r => r.size);

                    // Ýkinci geçiþ: tarama yap
                    long scannedMemory = 0;
                    int lastPercent = 0;

                    foreach (var region in regionsToScan)
                    {
                        ScanRegion(hProcess, region, targets, overlap, foundAddresses, ref scannedMemory, totalMemoryToScan, ref lastPercent, progress);
                    }

                    _logger?.Log($"Tarama tamamlandý: {regionsToScan.Count} bölge, {foundAddresses.Count} eþleþme");
                    progress?.Report(100);
                }
                finally
                {
                    _processManager.CloseProcessHandle(hProcess);
                }
            }
            catch (Exception ex)
            {
                _logger?.SetStatus("Hata: " + ex.Message);
                _logger?.LogError("ScanMemory exception", ex);
            }

            return foundAddresses;
        }

        private List<(IntPtr baseAddr, long size)> GetWritableRegions(IntPtr hProcess, int structSize)
        {
            var regions = new List<(IntPtr baseAddr, long size)>();
            IntPtr address = IntPtr.Zero;

            while (true)
            {
                MEMORY_BASIC_INFORMATION mbi;
                var res = VirtualQueryEx(hProcess, address, out mbi, (UIntPtr)structSize);
                if (res == UIntPtr.Zero)
                    break;

                long regionSize = mbi.RegionSize.ToInt64();
                bool isCommitted = (mbi.State & MEM_COMMIT) != 0;
                bool noAccess = (mbi.Protect & PAGE_NOACCESS) != 0;
                bool guard = (mbi.Protect & PAGE_GUARD) != 0;
                bool isReadable = isCommitted && !noAccess && !guard;

                if (isReadable)
                {
                    uint prot = mbi.Protect;
                    bool writable = ((prot & PAGE_READWRITE) != 0) ||
                                   ((prot & PAGE_WRITECOPY) != 0) ||
                                   ((prot & PAGE_EXECUTE_READWRITE) != 0) ||
                                   ((prot & PAGE_EXECUTE_WRITECOPY) != 0);

                    if (writable && regionSize > 0)
                    {
                        regions.Add((mbi.BaseAddress, regionSize));
                    }
                }

                long next = mbi.BaseAddress.ToInt64() + mbi.RegionSize.ToInt64();
                if (next >= long.MaxValue)
                    break;
                address = new IntPtr(next);
            }

            return regions;
        }

        private void ScanRegion(IntPtr hProcess, (IntPtr baseAddr, long size) region, List<(string subtype, byte[] bytes)> targets,
            int overlap, List<FoundAddress> foundAddresses, ref long scannedMemory, long totalMemoryToScan, ref int lastPercent, IProgress<int> progress)
        {
            long regionSize = region.size;
            long offset = 0;
            const int chunk = 0x10000; // 64KB
            int step = Math.Max(1, chunk - overlap);

            while (offset < regionSize)
            {
                int toRead = (int)Math.Min(chunk, regionSize - offset);
                byte[] buffer = new byte[toRead];
                IntPtr bytesRead;
                IntPtr readAddress = new IntPtr(region.baseAddr.ToInt64() + offset);

                try
                {
                    bool okRead = ReadProcessMemory(hProcess, readAddress, buffer, toRead, out bytesRead);
                    if (okRead)
                    {
                        int actualRead = bytesRead.ToInt32();
                        if (actualRead > 0)
                        {
                            SearchBufferForTargets(buffer, actualRead, targets, region.baseAddr.ToInt64() + offset, foundAddresses);
                        }
                    }
                }
                catch { }

                offset += step;
                scannedMemory += step;

                // Progress güncelle
                if (totalMemoryToScan > 0 && progress != null)
                {
                    int percent = (int)((scannedMemory * 100) / totalMemoryToScan);
                    if (percent > lastPercent)
                    {
                        lastPercent = percent;
                        progress.Report(Math.Min(percent, 99));
                    }
                }
            }
        }

        private void SearchBufferForTargets(byte[] buffer, int actualRead, List<(string subtype, byte[] bytes)> targets, long baseOffset, List<FoundAddress> foundAddresses)
        {
            foreach (var t in targets)
            {
                int tlen = t.bytes.Length;
                if (tlen == 0 || actualRead < tlen)
                    continue;

                for (int i = 0; i <= actualRead - tlen; i++)
                {
                    bool ok = true;
                    for (int j = 0; j < tlen; j++)
                    {
                        if (buffer[i + j] != t.bytes[j])
                        {
                            ok = false;
                            break;
                        }
                    }

                    if (ok)
                    {
                        long foundAddrInt = baseOffset + i;
                        IntPtr foundAddr = new IntPtr(foundAddrInt);
                        foundAddresses.Add(new FoundAddress
                        {
                            Address = foundAddr,
                            Subtype = t.subtype,
                            Value = t.bytes
                        });
                    }
                }
            }
        }

        public List<FoundAddress> RescanAddresses(Process process, List<FoundAddress> addresses, string newValue)
        {
            var remaining = new List<FoundAddress>();

            try
            {
                IntPtr hProcess = _processManager.OpenProcessForRead(process.Id);
                if (hProcess == IntPtr.Zero)
                {
                    _logger?.SetStatus($"{process.ProcessName}: açýlmadý");
                    _logger?.Log("OpenProcess returned zero for rescan.");
                    return remaining;
                }

                try
                {
                    foreach (var fa in addresses)
                    {
                        if (!_valueConverter.GetBytesForType(fa.Subtype, newValue, out byte[] newBytes, out string err))
                        {
                            _logger?.Log($"Parse new value for subtype {fa.Subtype} failed: {err}");
                            continue;
                        }

                        byte[] buffer = new byte[newBytes.Length];
                        IntPtr bytesRead;

                        try
                        {
                            bool okRead = ReadProcessMemory(hProcess, fa.Address, buffer, buffer.Length, out bytesRead);
                            if (!okRead)
                            {
                                int errc = Marshal.GetLastWin32Error();
                                _logger?.Log($"Rescan ReadProcessMemory failed at 0x{fa.Address.ToString("X")}, Err={errc}");
                                continue;
                            }

                            int actualRead = bytesRead.ToInt32();
                            _logger?.Log($"Rescan read {actualRead} bytes at 0x{fa.Address.ToString("X")} subtype={fa.Subtype}");

                            bool match = buffer.SequenceEqual(newBytes);
                            if (match)
                            {
                                remaining.Add(new FoundAddress
                                {
                                    Address = fa.Address,
                                    Subtype = fa.Subtype,
                                    Value = newBytes
                                });
                                _logger?.Log($"Rescan match at 0x{fa.Address.ToString("X")}");
                            }
                        }
                        catch (Exception ex)
                        {
                            _logger?.LogError($"Exception in rescan reading 0x{fa.Address.ToString("X")}", ex);
                        }
                    }
                }
                finally
                {
                    _processManager.CloseProcessHandle(hProcess);
                }
            }
            catch (Exception ex)
            {
                _logger?.SetStatus("Hata: " + ex.Message);
                _logger?.LogError("RescanAddresses exception", ex);
            }

            return remaining;
        }
    }
}
