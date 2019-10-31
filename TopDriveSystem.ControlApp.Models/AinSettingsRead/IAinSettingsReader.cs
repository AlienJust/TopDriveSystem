using System;
using TopDriveSystem.Commands.AinSettings;

namespace TopDriveSystem.ControlApp.Models.AinSettingsRead
{
    /// <summary>
    ///     Интерфейс чтения и настроек АИН
    /// </summary>
    public interface IAinSettingsReader
    {
        /// <summary>
        /// </summary>
        /// <param name="zeroBasedAinNumber">Номер АИН</param>
        /// <param name="callback">
        ///     Метод обратного вызова, в который передаётся внутреннее исключение (если возникло) и
        ///     результирующие настройки
        /// </param>
        /// <param name="forceRead">Флаг, указывающий не использовать настройки считанные ранее</param>
        void ReadSettingsAsync(byte zeroBasedAinNumber, bool forceRead, Action<Exception, IAinSettings> callback);
    }
}