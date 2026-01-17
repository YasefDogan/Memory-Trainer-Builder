using System;

namespace cheat_engine.Interfaces
{
    /// <summary>
    /// Loglama iþlemlerini tanýmlayan interface
    /// </summary>
    public interface ILogger
    {
        /// <summary>
        /// Log mesajý ekler
        /// </summary>
        /// <param name="message">Log mesajý</param>
        void Log(string message);

        /// <summary>
        /// Hata mesajý loglar
        /// </summary>
        /// <param name="message">Hata mesajý</param>
        /// <param name="exception">Ýstisna (opsiyonel)</param>
        void LogError(string message, Exception exception = null);

        /// <summary>
        /// Uyarý mesajý loglar
        /// </summary>
        /// <param name="message">Uyarý mesajý</param>
        void LogWarning(string message);

        /// <summary>
        /// Bilgi mesajý loglar
        /// </summary>
        /// <param name="message">Bilgi mesajý</param>
        void LogInfo(string message);

        /// <summary>
        /// Durum metnini günceller
        /// </summary>
        /// <param name="status">Durum metni</param>
        void SetStatus(string status);
    }
}
