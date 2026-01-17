using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace cheat_engine.Interfaces
{
    /// <summary>
    /// Bellek tarama iþlemlerini tanýmlayan interface
    /// </summary>
    public interface IMemoryScanner
    {
        /// <summary>
        /// Process belleðini tarar ve eþleþen adresleri bulur
        /// </summary>
        /// <param name="process">Taranacak process</param>
        /// <param name="targets">Aranacak hedefler (tip ve byte dizisi)</param>
        /// <param name="progress">Ýlerleme raporlayýcý</param>
        /// <returns>Bulunan adresler</returns>
        List<FoundAddress> ScanMemory(Process process, List<(string subtype, byte[] bytes)> targets, IProgress<int> progress = null);

        /// <summary>
        /// Daha önce bulunan adresleri yeni deðerle tekrar tarar
        /// </summary>
        /// <param name="process">Taranacak process</param>
        /// <param name="addresses">Tekrar taranacak adresler</param>
        /// <param name="newValue">Yeni aranacak deðer</param>
        /// <returns>Eþleþen adresler</returns>
        List<FoundAddress> RescanAddresses(Process process, List<FoundAddress> addresses, string newValue);
    }
}
