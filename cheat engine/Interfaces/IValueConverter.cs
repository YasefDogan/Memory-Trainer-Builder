using System;
using System.Collections.Generic;

namespace cheat_engine.Interfaces
{
    /// <summary>
    /// Deðer dönüþüm iþlemlerini tanýmlayan interface
    /// </summary>
    public interface IValueConverter
    {
        /// <summary>
        /// Metin deðerini belirtilen tipe göre byte dizisine çevirir
        /// </summary>
        /// <param name="type">Veri tipi</param>
        /// <param name="text">Metin deðer</param>
        /// <param name="data">Çýktý byte dizisi</param>
        /// <param name="error">Hata mesajý</param>
        /// <returns>Baþarýlý mý?</returns>
        bool GetBytesForType(string type, string text, out byte[] data, out string error);

        /// <summary>
        /// Int tipi için tüm alt tipleri dener ve geçerli olanlarý döndürür
        /// </summary>
        /// <param name="type">Ana tip</param>
        /// <param name="valueText">Deðer metni</param>
        /// <param name="error">Hata mesajý</param>
        /// <returns>Geçerli tip ve byte dizisi çiftleri</returns>
        List<(string subtype, byte[] bytes)> BuildTargets(string type, string valueText, out string error);

        /// <summary>
        /// Byte dizisini belirtilen tipe göre okunabilir metne çevirir
        /// </summary>
        /// <param name="type">Veri tipi</param>
        /// <param name="data">Byte dizisi</param>
        /// <returns>Okunabilir metin</returns>
        string FormatBytesAsDisplay(string type, byte[] data);

        /// <summary>
        /// Kýsa tip adýný tam tip adýna çevirir
        /// </summary>
        /// <param name="shortType">Kýsa tip adý (ör: int32)</param>
        /// <returns>Tam tip adý (ör: Int32)</returns>
        string MapShortToSubtype(string shortType);

        /// <summary>
        /// Tam tip adýný kýsa tip adýna çevirir
        /// </summary>
        /// <param name="subtype">Tam tip adý (ör: Int32)</param>
        /// <returns>Kýsa tip adý (ör: int32)</returns>
        string MapSubtypeToShort(string subtype);

        /// <summary>
        /// Belirtilen tip için byte uzunluðunu döndürür
        /// </summary>
        /// <param name="subtype">Veri tipi</param>
        /// <returns>Byte uzunluðu</returns>
        int GetByteLengthForType(string subtype);
    }
}
