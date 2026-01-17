using System;
using System.Collections.Generic;

namespace cheat_engine.Interfaces
{
    /// <summary>
    /// Adres dosyasý iþlemlerini tanýmlayan interface
    /// </summary>
    public interface IAddressFileManager
    {
        /// <summary>
        /// Adresleri dosyaya kaydeder
        /// </summary>
        /// <param name="addresses">Kaydedilecek adresler</param>
        /// <param name="filePath">Dosya yolu</param>
        void ExportAddresses(List<FoundAddress> addresses, string filePath);

        /// <summary>
        /// Dosyadan adresleri okur
        /// </summary>
        /// <param name="filePath">Dosya yolu</param>
        /// <returns>Okunan adresler</returns>
        List<FoundAddress> ImportAddresses(string filePath);

        /// <summary>
        /// Dosyayý uploads klasörüne kopyalar
        /// </summary>
        /// <param name="sourcePath">Kaynak dosya yolu</param>
        /// <returns>Hedef dosya yolu</returns>
        string UploadFile(string sourcePath);

        /// <summary>
        /// Uploads klasöründeki dosyalarý listeler
        /// </summary>
        /// <returns>Dosya yollarý listesi</returns>
        IEnumerable<string> GetUploadedFiles();
    }
}
