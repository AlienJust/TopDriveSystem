using AlienJust.Support.Mvvm;
using TopDriveSystem.Commands.BsEthernetSettings;

namespace TopDriveSystem.ConfigApp.BsEthernetSettings
{
    internal class FtRoleViewModel : ViewModelBase
    {
        public FtRoleViewModel(FriquencyTransformerRole role)
        {
            Role = role;
        }

        public string Text => Role.ToText();

        public FriquencyTransformerRole Role { get; }
    }
}