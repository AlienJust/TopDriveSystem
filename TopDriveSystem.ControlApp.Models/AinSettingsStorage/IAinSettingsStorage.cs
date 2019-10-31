using TopDriveSystem.Commands.AinSettings;

namespace TopDriveSystem.ControlApp.Models.AinSettingsStorage
{
    /// <summary>
    ///     Хранилище настроек используется прежде всего для записи настроек АИН.
    ///     Пока нет настроек в хранилище, запись настроек осуществаляется не может.
    /// </summary>
    public interface IAinSettingsStorage
    {
        IAinSettings GetSettings(byte zeroBasedAinNumber);
    }
}