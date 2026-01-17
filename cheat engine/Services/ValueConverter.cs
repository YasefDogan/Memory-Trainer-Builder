using System;
using System.Globalization;
using System.Collections.Generic;
using cheat_engine.Interfaces;

namespace cheat_engine.Services
{
    /// <summary>
    /// Deðer dönüþüm iþlemlerinin implementasyonu
    /// </summary>
    public class ValueConverter : IValueConverter
    {
        public bool GetBytesForType(string type, string text, out byte[] data, out string error)
        {
            data = null;
            error = null;
            try
            {
                switch (type)
                {
                    case "Int8":
                        if (sbyte.TryParse(text, out sbyte sbyteVal))
                            data = new byte[] { unchecked((byte)sbyteVal) };
                        else { error = "Geçersiz Int8"; return false; }
                        break;
                    case "UInt8":
                        if (byte.TryParse(text, out byte byteVal))
                            data = new byte[] { byteVal };
                        else { error = "Geçersiz UInt8"; return false; }
                        break;
                    case "Int16":
                        if (short.TryParse(text, out short shortVal))
                            data = BitConverter.GetBytes(shortVal);
                        else { error = "Geçersiz Int16"; return false; }
                        break;
                    case "UInt16":
                        if (ushort.TryParse(text, out ushort ushortVal))
                            data = BitConverter.GetBytes(ushortVal);
                        else { error = "Geçersiz UInt16"; return false; }
                        break;
                    case "Int32":
                        if (int.TryParse(text, out int int32Val))
                            data = BitConverter.GetBytes(int32Val);
                        else { error = "Geçersiz Int32"; return false; }
                        break;
                    case "UInt32":
                        if (uint.TryParse(text, out uint uint32Val))
                            data = BitConverter.GetBytes(uint32Val);
                        else { error = "Geçersiz UInt32"; return false; }
                        break;
                    case "Int64":
                        if (long.TryParse(text, out long int64Val))
                            data = BitConverter.GetBytes(int64Val);
                        else { error = "Geçersiz Int64"; return false; }
                        break;
                    case "UInt64":
                        if (ulong.TryParse(text, out ulong uint64Val))
                            data = BitConverter.GetBytes(uint64Val);
                        else { error = "Geçersiz UInt64"; return false; }
                        break;
                    case "Float32":
                        if (float.TryParse(text, NumberStyles.Float, CultureInfo.InvariantCulture, out float floatVal))
                            data = BitConverter.GetBytes(floatVal);
                        else { error = "Geçersiz Float32"; return false; }
                        break;
                    case "Float64":
                        if (double.TryParse(text, NumberStyles.Float, CultureInfo.InvariantCulture, out double doubleVal))
                            data = BitConverter.GetBytes(doubleVal);
                        else { error = "Geçersiz Float64"; return false; }
                        break;
                    case "Bytes (Hex)":
                        {
                            string hex = text.Replace(" ", string.Empty).Replace("-", string.Empty);
                            if (hex.StartsWith("0x", StringComparison.OrdinalIgnoreCase))
                                hex = hex.Substring(2);
                            if (hex.Length % 2 != 0)
                                hex = "0" + hex;
                            try
                            {
                                data = new byte[hex.Length / 2];
                                for (int idx = 0; idx < data.Length; idx++)
                                    data[idx] = byte.Parse(hex.Substring(idx * 2, 2), NumberStyles.HexNumber);
                            }
                            catch { error = "Geçersiz hex bayt dizisi"; return false; }
                        }
                        break;
                    default:
                        error = "Bilinmeyen tip";
                        return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return false;
            }
        }

        public List<(string subtype, byte[] bytes)> BuildTargets(string type, string valueText, out string error)
        {
            error = null;
            var list = new List<(string, byte[])>();

            if (type == "Int")
            {
                string[] intSubtypes = new[] { "Int8", "UInt8", "Int16", "UInt16", "Int32", "UInt32", "Int64", "UInt64" };
                foreach (var s in intSubtypes)
                {
                    if (GetBytesForType(s, valueText, out byte[] bytes, out string err))
                        list.Add((s, bytes));
                }
                if (list.Count == 0)
                    error = "Geçersiz Int deðeri";
                return list;
            }
            else
            {
                if (GetBytesForType(type, valueText, out byte[] bytes, out string err))
                    list.Add((type, bytes));
                else
                    error = err;
                return list;
            }
        }

        public string FormatBytesAsDisplay(string type, byte[] data)
        {
            try
            {
                switch (type)
                {
                    case "Int8": return ((sbyte)data[0]).ToString();
                    case "UInt8": return data[0].ToString();
                    case "Int16": return BitConverter.ToInt16(data, 0).ToString();
                    case "UInt16": return BitConverter.ToUInt16(data, 0).ToString();
                    case "Int32": return BitConverter.ToInt32(data, 0).ToString();
                    case "UInt32": return BitConverter.ToUInt32(data, 0).ToString();
                    case "Int64": return BitConverter.ToInt64(data, 0).ToString();
                    case "UInt64": return BitConverter.ToUInt64(data, 0).ToString();
                    case "Float32": return BitConverter.ToSingle(data, 0).ToString(CultureInfo.InvariantCulture);
                    case "Float64": return BitConverter.ToDouble(data, 0).ToString(CultureInfo.InvariantCulture);
                    case "Bytes (Hex)": return "0x" + BitConverter.ToString(data).Replace("-", "");
                    default: return BitConverter.ToString(data);
                }
            }
            catch
            {
                return BitConverter.ToString(data);
            }
        }

        public string MapShortToSubtype(string shortType)
        {
            if (string.IsNullOrEmpty(shortType))
                return "Int32";

            shortType = shortType.Trim().ToLowerInvariant();
            switch (shortType)
            {
                case "int8": return "Int8";
                case "uint8": return "UInt8";
                case "int16": return "Int16";
                case "uint16": return "UInt16";
                case "int32":
                case "int": return "Int32";
                case "uint32": return "UInt32";
                case "int64": return "Int64";
                case "uint64": return "UInt64";
                case "float":
                case "float32": return "Float32";
                case "double":
                case "float64": return "Float64";
                case "bytes":
                case "hex":
                case "bytes(hex)": return "Bytes (Hex)";
                default: return shortType;
            }
        }

        public string MapSubtypeToShort(string subtype)
        {
            switch (subtype)
            {
                case "Int8": return "int8";
                case "UInt8": return "uint8";
                case "Int16": return "int16";
                case "UInt16": return "uint16";
                case "Int32": return "int32";
                case "UInt32": return "uint32";
                case "Int64": return "int64";
                case "UInt64": return "uint64";
                case "Float32": return "float";
                case "Float64": return "double";
                case "Bytes (Hex)": return "bytes";
                default: return subtype;
            }
        }

        public int GetByteLengthForType(string subtype)
        {
            switch (subtype)
            {
                case "Int8":
                case "UInt8":
                    return 1;
                case "Int16":
                case "UInt16":
                    return 2;
                case "Int32":
                case "UInt32":
                case "Float32":
                    return 4;
                case "Int64":
                case "UInt64":
                case "Float64":
                    return 8;
                default:
                    return 4;
            }
        }
    }
}
