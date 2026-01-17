using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.InteropServices;
using cheat_engine.Interfaces;

namespace cheat_engine.Services
{
    /// <summary>
    /// Bellek yazma iþlemlerinin implementasyonu
    /// </summary>
    public class MemoryWriter : IMemoryWriter
    {
        private readonly IProcessManager _processManager;
        private readonly IValueConverter _valueConverter;
        private readonly ILogger _logger;

        // P/Invoke declarations
        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, [Out] byte[] lpBuffer, int dwSize, out IntPtr lpNumberOfBytesRead);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, int nSize, out IntPtr lpNumberOfBytesWritten);

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

        public MemoryWriter(IProcessManager processManager, IValueConverter valueConverter, ILogger logger)
        {
            _processManager = processManager;
            _valueConverter = valueConverter;
            _logger = logger;
        }

        public List<WriteResult> WriteToAddresses(Process process, List<FoundAddress> addresses, byte[] data)
        {
            var results = new List<WriteResult>();

            try
            {
                IntPtr hProcess = _processManager.OpenProcessForWrite(process.Id);
                if (hProcess == IntPtr.Zero)
                {
                    _logger?.SetStatus($"{process.ProcessName}: yazma için açýlamadý (izin yok)");
                    _logger?.Log("OpenProcess for write failed.");
                    return results;
                }

                try
                {
                    foreach (var fa in addresses)
                    {
                        var result = WriteToAddressInternal(hProcess, fa, data);
                        results.Add(result);
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
                _logger?.LogError("WriteToAddresses exception", ex);
            }

            return results;
        }

        public WriteResult WriteToAddress(Process process, FoundAddress address, byte[] data)
        {
            try
            {
                IntPtr hProcess = _processManager.OpenProcessForWrite(process.Id);
                if (hProcess == IntPtr.Zero)
                {
                    return new WriteResult
                    {
                        Address = address.Address,
                        Success = false,
                        ErrorMessage = "Process açýlamadý",
                        ErrorCode = Marshal.GetLastWin32Error()
                    };
                }

                try
                {
                    // Önce bölgenin yazýlabilir olup olmadýðýný kontrol et
                    if (!IsRegionWritable(hProcess, address.Address))
                    {
                        return new WriteResult
                        {
                            Address = address.Address,
                            Success = false,
                            ErrorMessage = "Bu adres yazýlabilir deðil"
                        };
                    }

                    return WriteToAddressInternal(hProcess, address, data);
                }
                finally
                {
                    _processManager.CloseProcessHandle(hProcess);
                }
            }
            catch (Exception ex)
            {
                _logger?.LogError($"WriteToAddress exception at 0x{address.Address.ToString("X")}", ex);
                return new WriteResult
                {
                    Address = address.Address,
                    Success = false,
                    ErrorMessage = ex.Message
                };
            }
        }

        private WriteResult WriteToAddressInternal(IntPtr hProcess, FoundAddress address, byte[] data)
        {
            var result = new WriteResult { Address = address.Address };

            try
            {
                IntPtr written;
                bool ok = WriteProcessMemory(hProcess, address.Address, data, data.Length, out written);

                if (ok)
                {
                    result.Success = true;
                    result.BytesWritten = written.ToInt32();
                    _logger?.Log($"Wrote {written.ToInt32()} bytes to 0x{address.Address.ToString("X")}.");
                }
                else
                {
                    result.Success = false;
                    result.ErrorCode = Marshal.GetLastWin32Error();
                    result.ErrorMessage = $"WriteProcessMemory failed, Err={result.ErrorCode}";
                    _logger?.Log($"WriteProcessMemory failed at 0x{address.Address.ToString("X")} Err={result.ErrorCode}");
                }
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
                _logger?.LogError($"Exception writing to 0x{address.Address.ToString("X")}", ex);
            }

            return result;
        }

        private bool IsRegionWritable(IntPtr hProcess, IntPtr address)
        {
            MEMORY_BASIC_INFORMATION mbi;
            var res = VirtualQueryEx(hProcess, address, out mbi, (UIntPtr)Marshal.SizeOf(typeof(MEMORY_BASIC_INFORMATION)));

            if (res == UIntPtr.Zero)
                return false;

            bool isCommitted = (mbi.State & MEM_COMMIT) != 0;
            bool noAccess = (mbi.Protect & PAGE_NOACCESS) != 0;
            bool guard = (mbi.Protect & PAGE_GUARD) != 0;
            uint prot = mbi.Protect;
            bool writable = ((prot & PAGE_READWRITE) != 0) ||
                           ((prot & PAGE_WRITECOPY) != 0) ||
                           ((prot & PAGE_EXECUTE_READWRITE) != 0) ||
                           ((prot & PAGE_EXECUTE_WRITECOPY) != 0);

            return isCommitted && !noAccess && !guard && writable;
        }

        public string ReadAddressValue(Process process, FoundAddress address)
        {
            try
            {
                IntPtr hProcess = _processManager.OpenProcessForRead(process.Id);
                if (hProcess == IntPtr.Zero)
                {
                    _logger?.Log($"OpenProcess fail Err={Marshal.GetLastWin32Error()}");
                    return null;
                }

                try
                {
                    int len = _valueConverter.GetByteLengthForType(address.Subtype);

                    MEMORY_BASIC_INFORMATION mbi;
                    var res = VirtualQueryEx(hProcess, address.Address, out mbi, (UIntPtr)Marshal.SizeOf(typeof(MEMORY_BASIC_INFORMATION)));
                    if (res == UIntPtr.Zero)
                    {
                        _logger?.Log("VirtualQueryEx fail");
                        return null;
                    }

                    if ((mbi.State & MEM_COMMIT) == 0 || (mbi.Protect & PAGE_NOACCESS) != 0 || (mbi.Protect & PAGE_GUARD) != 0)
                        return null;

                    byte[] buffer = new byte[len];
                    IntPtr bytesRead;
                    if (!ReadProcessMemory(hProcess, address.Address, buffer, len, out bytesRead))
                    {
                        _logger?.Log($"Read fail Err={Marshal.GetLastWin32Error()}");
                        return null;
                    }

                    if (bytesRead.ToInt32() <= 0)
                        return null;

                    return FormatBufferByType(address.Subtype, buffer);
                }
                finally
                {
                    _processManager.CloseProcessHandle(hProcess);
                }
            }
            catch (Exception ex)
            {
                _logger?.LogError("ReadAddressValue exception", ex);
                return null;
            }
        }

        private string FormatBufferByType(string subtype, byte[] buffer)
        {
            switch (subtype)
            {
                case "Int8": return ((sbyte)buffer[0]).ToString();
                case "UInt8": return buffer[0].ToString();
                case "Int16": return BitConverter.ToInt16(buffer, 0).ToString();
                case "UInt16": return BitConverter.ToUInt16(buffer, 0).ToString();
                case "Int32": return BitConverter.ToInt32(buffer, 0).ToString();
                case "UInt32": return BitConverter.ToUInt32(buffer, 0).ToString();
                case "Int64": return BitConverter.ToInt64(buffer, 0).ToString();
                case "UInt64": return BitConverter.ToUInt64(buffer, 0).ToString();
                case "Float32": return BitConverter.ToSingle(buffer, 0).ToString(CultureInfo.InvariantCulture);
                case "Float64": return BitConverter.ToDouble(buffer, 0).ToString(CultureInfo.InvariantCulture);
                default: return BitConverter.ToString(buffer);
            }
        }
    }
}
