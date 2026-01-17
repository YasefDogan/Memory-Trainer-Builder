using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace cheat_engine.Interfaces
{
    /// <summary>
    /// Process yönetim iþlemlerini tanýmlayan interface
    /// </summary>
    public interface IProcessManager
    {
        /// <summary>
        /// Sistemdeki tüm process'leri listeler
        /// </summary>
        /// <returns>Process listesi</returns>
        IEnumerable<ProcessInfo> GetProcessList();

        /// <summary>
        /// Belirtilen ID'ye sahip process'i döndürür
        /// </summary>
        /// <param name="processId">Process ID</param>
        /// <returns>Process veya null</returns>
        Process GetProcessById(int processId);

        /// <summary>
        /// Process'in hala çalýþýp çalýþmadýðýný kontrol eder
        /// </summary>
        /// <param name="processId">Process ID</param>
        /// <returns>Çalýþýyor mu?</returns>
        bool IsProcessRunning(int processId);

        /// <summary>
        /// Process'i okuma için açar
        /// </summary>
        /// <param name="processId">Process ID</param>
        /// <returns>Process handle veya IntPtr.Zero</returns>
        IntPtr OpenProcessForRead(int processId);

        /// <summary>
        /// Process'i yazma için açar
        /// </summary>
        /// <param name="processId">Process ID</param>
        /// <returns>Process handle veya IntPtr.Zero</returns>
        IntPtr OpenProcessForWrite(int processId);

        /// <summary>
        /// Process handle'ý kapatýr
        /// </summary>
        /// <param name="handle">Kapatýlacak handle</param>
        void CloseProcessHandle(IntPtr handle);
    }

    /// <summary>
    /// Process bilgisini temsil eder
    /// </summary>
    public class ProcessInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Process Process { get; set; }

        public override string ToString()
        {
            return $"{Name} ({Id})";
        }
    }
}
