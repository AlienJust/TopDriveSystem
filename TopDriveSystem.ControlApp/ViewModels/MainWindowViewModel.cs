using System.ComponentModel;
using System.Reactive;
using System.Reactive.Linq;
using System.Runtime.Serialization;
using System.Windows.Input;
using ReactiveUI;
using Splat;
using TopDriveSystem.ControlApp.ViewModels.AinsCountSelection;
using TopDriveSystem.ControlApp.ViewModels.DeviceConnection;
using TopDriveSystem.ControlApp.ViewModels.Settings;
using TopDriveSystem.ControlApp.ViewModels.Telemetry;

namespace TopDriveSystem.ControlApp.ViewModels
{
    public class MainWindowViewModel : ReactiveObject, IScreen, IMainViewModel
    {
        private readonly ReactiveCommand<Unit, Unit> _control;
        private readonly ReactiveCommand<Unit, Unit> _settings;
        private readonly ReactiveCommand<Unit, Unit> _telemetry;

        private RoutingState _router = new RoutingState();

        public MainWindowViewModel(IDeviceConnectionViewModel deviceConnectionViewModel = null,
            IAinsCountSelectionViewModel ainsCountSelectionViewModel = null)
        {
            DeviceConnectionVm = deviceConnectionViewModel ?? Locator.Current.GetService<IDeviceConnectionViewModel>();
            AinsCountSelectionVm =
                ainsCountSelectionViewModel ?? Locator.Current.GetService<IAinsCountSelectionViewModel>();

            var canTelemetry = this
                .WhenAnyObservable(x => x.Router.CurrentViewModel)
                .Select(current => !(current is ITelemetry100ViewModel));

            _telemetry = ReactiveCommand.Create(
                () => { Router.Navigate.Execute(new Telemetry100ViewModel()); },
                canTelemetry);


            var canSettings = this
                .WhenAnyObservable(x => x.Router.CurrentViewModel)
                .Select(current => !(current is ISettingsViewModel));

            _settings = ReactiveCommand.Create(
                () => { Router.Navigate.Execute(new SettingsViewModel()); },
                canSettings);
        }


        public IDeviceConnectionViewModel DeviceConnectionVm { get; }
        public IAinsCountSelectionViewModel AinsCountSelectionVm { get; }

        public ICommand Settings => _settings;

        public ICommand Telemetry => _telemetry;

        public ICommand Control => _control;

        [DataMember]
        public RoutingState Router
        {
            get => _router;
            set => this.RaiseAndSetIfChanged(ref _router, value);
        }
    }

    public interface IMainViewModel : INotifyPropertyChanged, IScreen
    {
        IDeviceConnectionViewModel DeviceConnectionVm { get; }
        IAinsCountSelectionViewModel AinsCountSelectionVm { get; }

        ICommand Settings { get; }

        ICommand Telemetry { get; }

        ICommand Control { get; }
    }
}