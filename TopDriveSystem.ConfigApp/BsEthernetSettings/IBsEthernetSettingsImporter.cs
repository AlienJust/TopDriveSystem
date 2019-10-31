using TopDriveSystem.Commands.BsEthernetSettings;

namespace TopDriveSystem.ConfigApp.BsEthernetSettings
{
    public interface IBsEthernetSettingsImporter
    {
        IBsEthernetSettings ImportSettings();
    }
}