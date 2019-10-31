using System.Collections.Generic;
using System.Runtime.Serialization;
using ReactiveUI;
using Splat;
using TopDriveSystem.ControlApp.ViewModels.Parameter;

namespace TopDriveSystem.ControlApp.ViewModels.Telemetry
{
    [DataContract]
    public class Telemetry100ViewModel : ReactiveObject, ITelemetry100ViewModel, IParameterVMsHolder
    {
        private IReadOnlyDictionary<string, IParameterViewModel> _parameters;

        public Telemetry100ViewModel(IScreen screen = null, IParameterVMsHolder paramsHolder = null)
        {
            HostScreen = screen ?? Locator.Current.GetService<IScreen>();
            Parameters = paramsHolder != null
                ? paramsHolder.Parameters
                : Locator.Current.GetService<IParameterVMsHolder>().Parameters;
        }

        public string Name => "Группа телеметрии 100";

        public IReadOnlyDictionary<string, IParameterViewModel> Parameters
        {
            get => _parameters;
            set => this.RaiseAndSetIfChanged(ref _parameters, value);
        }

        public IScreen HostScreen { get; }

        public string UrlPathSegment => "/telemetry";
    }
}