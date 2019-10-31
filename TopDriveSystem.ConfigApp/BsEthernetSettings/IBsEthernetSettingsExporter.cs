using TopDriveSystem.Commands.BsEthernetSettings;

namespace TopDriveSystem.ConfigApp.BsEthernetSettings
{
    public interface IBsEthernetSettingsExporter
    {
        void ExportSettings(IBsEthernetSettings settings);
    }
}