using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using cheat_engine.Interfaces;

namespace cheat_engine.Services
{
    /// <summary>
    /// Process yönetim iþlemlerinin implementasyonu
    /// </summary>
    public class ProcessManager : IProcessManager
    {
        // P/Invoke declarations
        [DllImport("kernel32.dll")]
        private static extern IntPtr OpenProcess(int dwDesiredAccess, bool bInheritHandle, int dwProcessId);

        [DllImport("kernel32.dll")]
        private static extern bool CloseHandle(IntPtr hObject);

        private const int PROCESS_WM_READ = 0x0010;
        private const int PROCESS_QUERY_INFORMATION = 0x0400;
        private const int PROCESS_VM_OPERATION = 0x0008;
        private const int PROCESS_VM_WRITE = 0x0020;

        public IEnumerable<ProcessInfo> GetProcessList()
        {
            var processes = new List<ProcessInfo>();
            try
            {
                var procs = Process.GetProcesses().OrderBy(p => p.ProcessName).ToList();
                foreach (var p in procs)
                {
                    try
                    {
                        processes.Add(new ProcessInfo
                        {
                            Id = p.Id,
                            Name = p.ProcessName,
                            Process = p
                        });
                    }
                    catch { }
                }
            }
            catch { }
            return processes;
        }

        public Process GetProcessById(int processId)
        {
            try
            {
                return Process.GetProcessById(processId);
            }
            catch
            {
                return null;
            }
        }

        public bool IsProcessRunning(int processId)
        {
            try
            {
                var process = Process.GetProcessById(processId);
                return process != null && !process.HasExited;
            }
            catch
            {
                return false;
            }
        }

        public IntPtr OpenProcessForRead(int processId)
        {
            return OpenProcess(PROCESS_WM_READ | PROCESS_QUERY_INFORMATION | PROCESS_VM_OPERATION, false, processId);
        }

        public IntPtr OpenProcessForWrite(int processId)
        {
            return OpenProcess(PROCESS_VM_WRITE | PROCESS_VM_OPERATION | PROCESS_QUERY_INFORMATION | PROCESS_WM_READ, false, processId);
        }

        public void CloseProcessHandle(IntPtr handle)
        {
            if (handle != IntPtr.Zero)
            {
                CloseHandle(handle);
            }
        }
    }
}
