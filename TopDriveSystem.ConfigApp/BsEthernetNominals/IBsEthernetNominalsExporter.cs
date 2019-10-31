using TopDriveSystem.Commands.BsEthernetNominals;

namespace TopDriveSystem.ConfigApp.BsEthernetNominals
{
    public interface IBsEthernetNominalsExporter
    {
        void ExportSettings(IBsEthernetNominals nominals);
    }
}