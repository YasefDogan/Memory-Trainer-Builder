using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace cheat_engine.Interfaces
{
    /// <summary>
    /// Bellek yazma iþlemlerini tanýmlayan interface
    /// </summary>
    public interface IMemoryWriter
    {
        /// <summary>
        /// Belirtilen adreslere veri yazar
        /// </summary>
        /// <param name="process">Hedef process</param>
        /// <param name="addresses">Yazýlacak adresler</param>
        /// <param name="data">Yazýlacak veri</param>
        /// <returns>Yazma sonuçlarý (adres, baþarýlý mý, yazýlan byte sayýsý)</returns>
        List<WriteResult> WriteToAddresses(Process process, List<FoundAddress> addresses, byte[] data);

        /// <summary>
        /// Tek bir adrese veri yazar
        /// </summary>
        /// <param name="process">Hedef process</param>
        /// <param name="address">Yazýlacak adres</param>
        /// <param name="data">Yazýlacak veri</param>
        /// <returns>Yazma sonucu</returns>
        WriteResult WriteToAddress(Process process, FoundAddress address, byte[] data);

        /// <summary>
        /// Belirtilen adresten deðer okur
        /// </summary>
        /// <param name="process">Hedef process</param>
        /// <param name="address">Okunacak adres</param>
        /// <returns>Okunan deðer (string olarak) veya null</returns>
        string ReadAddressValue(Process process, FoundAddress address);
    }

    /// <summary>
    /// Yazma iþlemi sonucunu temsil eder
    /// </summary>
    public class WriteResult
    {
        public IntPtr Address { get; set; }
        public bool Success { get; set; }
        public int BytesWritten { get; set; }
        public string ErrorMessage { get; set; }
        public int ErrorCode { get; set; }
    }
}
