using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using cheat_engine.Interfaces;

namespace cheat_engine.Services
{
    /// <summary>
    /// Adres dosyasý iþlemlerinin implementasyonu
    /// </summary>
    public class AddressFileManager : IAddressFileManager
    {
        private readonly IValueConverter _valueConverter;
        private readonly ILogger _logger;
        private readonly string _uploadsDirectory;

        public AddressFileManager(IValueConverter valueConverter, ILogger logger)
        {
            _valueConverter = valueConverter;
            _logger = logger;
            _uploadsDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "uploads");
            EnsureUploadsDirectoryExists();
        }

        private void EnsureUploadsDirectoryExists()
        {
            try
            {
                if (!Directory.Exists(_uploadsDirectory))
                    Directory.CreateDirectory(_uploadsDirectory);
            }
            catch (Exception ex)
            {
                _logger?.LogError("Uploads klasörü oluþturulamadý", ex);
            }
        }

        public void ExportAddresses(List<FoundAddress> addresses, string filePath)
        {
            if (addresses == null || addresses.Count == 0)
                throw new ArgumentException("Kaydedilecek adres yok.");

            var lines = new List<string>();
            foreach (var fa in addresses)
            {
                string addr = $"0x{fa.Address.ToString("X")}";
                string shortType = _valueConverter.MapSubtypeToShort(fa.Subtype);

                if (!string.IsNullOrEmpty(fa.Name))
                    lines.Add($"{addr} {shortType} \"{fa.Name}\"");
                else
                    lines.Add($"{addr} {shortType}");
            }

            File.WriteAllLines(filePath, lines, Encoding.UTF8);
            _logger?.LogInfo($"Adresler dosyaya yazýldý: {filePath}");
        }

        public List<FoundAddress> ImportAddresses(string filePath)
        {
            var addresses = new List<FoundAddress>();

            if (!File.Exists(filePath))
            {
                _logger?.LogWarning($"Dosya bulunamadý: {filePath}");
                return addresses;
            }

            var lines = File.ReadAllLines(filePath);
            foreach (var ln in lines)
            {
                var s = ln.Trim();
                if (string.IsNullOrEmpty(s))
                    continue;

                // Týrnak içindeki ismi ayýkla
                string name = null;
                int quoteStart = s.IndexOf('"');
                int quoteEnd = s.LastIndexOf('"');
                if (quoteStart >= 0 && quoteEnd > quoteStart)
                {
                    name = s.Substring(quoteStart + 1, quoteEnd - quoteStart - 1);
                    s = s.Substring(0, quoteStart).Trim();
                }

                var parts = s.Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length < 1)
                    continue;

                string addrPart = parts[0];
                string typePart = (parts.Length >= 2) ? parts[1] : "Int32";

                try
                {
                    if (addrPart.StartsWith("0x", StringComparison.OrdinalIgnoreCase))
                        addrPart = addrPart.Substring(2);

                    long val = long.Parse(addrPart, System.Globalization.NumberStyles.HexNumber);
                    IntPtr addr = new IntPtr(val);
                    string subtype = _valueConverter.MapShortToSubtype(typePart);

                    addresses.Add(new FoundAddress
                    {
                        Address = addr,
                        Subtype = subtype,
                        Name = name,
                        Value = null
                    });
                }
                catch (Exception ex)
                {
                    _logger?.LogError($"Satýr ayrýþtýrýlamadý: {ln}", ex);
                }
            }

            _logger?.LogInfo($"{addresses.Count} adres yüklendi: {filePath}");
            return addresses;
        }

        public string UploadFile(string sourcePath)
        {
            if (string.IsNullOrWhiteSpace(sourcePath) || !File.Exists(sourcePath))
                throw new FileNotFoundException("Dosya bulunamadý.", sourcePath);

            EnsureUploadsDirectoryExists();

            string destFileName = Path.GetFileName(sourcePath);
            string destPath = Path.Combine(_uploadsDirectory, destFileName);

            File.Copy(sourcePath, destPath, true);
            _logger?.LogInfo($"Dosya yüklendi: {destFileName}");

            return destPath;
        }

        public IEnumerable<string> GetUploadedFiles()
        {
            EnsureUploadsDirectoryExists();

            try
            {
                return Directory.GetFiles(_uploadsDirectory);
            }
            catch (Exception ex)
            {
                _logger?.LogError("Yüklenen dosyalar listelenemedi", ex);
                return new string[0];
            }
        }
    }
}
