using System.Runtime.Serialization;
using ReactiveUI;
using Splat;

namespace TopDriveSystem.ControlApp.ViewModels.Settings
{
    [DataContract]
    public class SettingsViewModel : ReactiveObject, ISettingsViewModel
    {
        public SettingsViewModel(IScreen screen = null)
        {
            HostScreen = screen ?? Locator.Current.GetService<IScreen>();
        }

        public string Name => "Настройки частотника";

        public IScreen HostScreen { get; }

        public string UrlPathSegment => "/settings";
    }
}