using TopDriveSystem.Commands.BsEthernetNominals;

namespace TopDriveSystem.ConfigApp.BsEthernetNominals
{
    public interface IBsEthernetNominalsImporter
    {
        IBsEthernetNominals ImportSettings();
    }
}