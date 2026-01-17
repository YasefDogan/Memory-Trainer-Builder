using System;

namespace cheat_engine
{
    /// <summary>
    /// Bulunan bellek adresini temsil eder
    /// </summary>
    public class FoundAddress
    {
        /// <summary>
        /// Bellek adresi
        /// </summary>
        public IntPtr Address { get; set; }

        /// <summary>
        /// Veri tipi (Int8, UInt8, Int16, UInt16, Int32, UInt32, Int64, UInt64, Float32, Float64, Bytes (Hex))
        /// </summary>
        public string Subtype { get; set; }

        /// <summary>
        /// Bulunan deðerin byte dizisi
        /// </summary>
        public byte[] Value { get; set; }

        /// <summary>
        /// Kullanýcý tarafýndan verilen özel isim (ör: "para", "can")
        /// </summary>
        public string Name { get; set; }

        public override string ToString()
        {
            string display = $"0x{Address.ToString("X")} | {Subtype}";
            if (!string.IsNullOrEmpty(Name))
                display = $"{Name}: {display}";
            return display;
        }

        public override bool Equals(object obj)
        {
            if (obj is FoundAddress other)
            {
                return Address == other.Address && Subtype == other.Subtype;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return Address.GetHashCode() ^ (Subtype?.GetHashCode() ?? 0);
        }
    }
}
