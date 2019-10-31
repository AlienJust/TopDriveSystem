using ReactiveUI;
using Splat;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TopDriveSystem.ControlApp.ViewModels.Settings
{
    [DataContract]
    public class SettingsViewModel : ReactiveObject, ISettingsViewModel
    {
        public string Name => "Настройки частотника";

        public IScreen HostScreen { get; }

        public string UrlPathSegment => "/settings";

        public SettingsViewModel(IScreen screen = null)
        {
            HostScreen = screen ?? Locator.Current.GetService<IScreen>();
        }
    }
}
