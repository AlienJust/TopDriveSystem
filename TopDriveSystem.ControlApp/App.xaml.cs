using System;
using AlienJust.Support.Concurrent.Contracts;
using Avalonia;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using Avalonia.Threading;
using DataAbstractionLevel.Low.PsnConfig;
using Microsoft.Extensions.DependencyInjection;
using ReactiveUI;
using Splat;
using TopDriveSystem.CommandSenders.Contracts;
using TopDriveSystem.ControlApp.Drivers;
using TopDriveSystem.ControlApp.Models.AinsCounter;
using TopDriveSystem.ControlApp.Models.CommandSenderHost;
using TopDriveSystem.ControlApp.Models.DeviceConnection;
using TopDriveSystem.ControlApp.Models.NotifySendingEnabled;
using TopDriveSystem.ControlApp.Models.ParamLogger;
using TopDriveSystem.ControlApp.Models.TargetAddressHost;
using TopDriveSystem.ControlApp.ViewModels;
using TopDriveSystem.ControlApp.ViewModels.AinsCountSelection;
using TopDriveSystem.ControlApp.ViewModels.DeviceConnection;
using TopDriveSystem.ControlApp.ViewModels.Parameter;
using TopDriveSystem.ControlApp.ViewModels.Settings;
using TopDriveSystem.ControlApp.ViewModels.Telemetry;
using TopDriveSystem.ControlApp.Views;
using TopDriveSystem.ControlApp.Views.Settings;
using TopDriveSystem.ControlApp.Views.Telemetry;
using TopDriveSystem.Model.Listening;
using TopDriveSystem.Parameters;

namespace TopDriveSystem.ControlApp
{
    public class App : Application
    {
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            // Register services. 
            // TODO: Move to project with models?
            var sp = BuildDependencies();

            // Reregistering things in Splat.
            Locator.CurrentMutable.RegisterConstant(sp.GetService<IParameterVMsHolder>());

            // Registering ViewModels in Splat locator.
            Locator.CurrentMutable.Register(() => sp.GetService<IDeviceConnectionViewModel>());
            Locator.CurrentMutable.Register(() => sp.GetService<IAinsCountSelectionViewModel>());

            var suspension = new AutoSuspendHelper(ApplicationLifetime);
            RxApp.SuspensionHost.CreateNewAppState = () => new MainWindowViewModel();
            RxApp.SuspensionHost.SetupDefaultSuspendResume(new NewtonsoftJsonSuspensionDriver("appstate.json"));
            suspension.OnFrameworkInitializationCompleted();

            Locator.CurrentMutable.RegisterConstant<IScreen>(RxApp.SuspensionHost.GetAppState<MainWindowViewModel>());

            // Used for Router.Navigate?
            Locator.CurrentMutable.Register<IViewFor<ITelemetry100ViewModel>>(() => new Telemetry100View());
            Locator.CurrentMutable.Register<IViewFor<ISettingsViewModel>>(() => new SettingsView());
            //Locator.CurrentMutable.Register<IViewFor<HelpViewModel>>(() => new HelpView());

            new MainWindow {DataContext = Locator.Current.GetService<IScreen>()}.Show();

            //ViewModels.ParameterPresentation.ParametersPresenterXmlSerializer.Serialize("params.xml", new PsnProtocolConfigurationLoaderFromXml("psn.Буровая.АИН1.xml").LoadConfiguration(), false);

            base.OnFrameworkInitializationCompleted();
        }

        private static ServiceProvider BuildDependencies()
        {
            var commandSenderHost = new CommandSenderHostThreadSafe();
            var targetAddressHost = new TargetAddressHostThreadSafe(1);
            var notifySendingEnabled = new NotifySendingEnabledThreadSafe(false);
            var paramLoggerAndRegPoint = new ParamLoggerRegistrationPointThreadSafe();

            var serviceCollection = new ServiceCollection()
                /*.AddLogging(loggingBuilder =>
                {
                    loggingBuilder.SetMinimumLevel(LogLevel.Trace);
                    loggingBuilder.AddNLog(new NLogProviderOptions
                    {
                        CaptureMessageTemplates = true,
                        CaptureMessageProperties = true
                    });
                })*/
                .AddSingleton<IThreadNotifier, AvaloniaThreadNotifier>()
                .AddSingleton<IParamLoggerRegistrationPoint>(sp => paramLoggerAndRegPoint)
                .AddSingleton<IParameterLogger>(sp => paramLoggerAndRegPoint)

                // Adding services.
                .AddSingleton<IIOListener>(sp => commandSenderHost)
                .AddSingleton<ICommandSenderHostSettable>(sp => commandSenderHost)
                .AddSingleton<ICommandSenderHost>(sp => commandSenderHost)
                .AddSingleton<ITargetAddressHostSettable>(sp => targetAddressHost)
                .AddSingleton<ITargetAddressHost>(sp => targetAddressHost)
                .AddSingleton<INotifySendingEnabledRaisable>(sp => notifySendingEnabled)
                .AddSingleton<INotifySendingEnabled>(sp => notifySendingEnabled)
                .AddSingleton<IAinsCounterRaisable>(sp => new AinsCounterThreadSafe(3))

                // TODO: need to change interface and to make proper implementation.
                .AddSingleton<IParameterSetter, ParameterSetterNothing>()

                // Adding PSN parameters classes.
                .AddSingleton(sp =>
                    new PsnProtocolConfigurationLoaderFromXml("psn.Буровая.АИН1.xml").LoadConfiguration())
                .AddSingleton<IPsnParamsList, PsnParamsListSimple>()
                .AddSingleton<IParamListener, CommandPartAndParamListenerSimple>()
                .AddSingleton<IParametersPresenterXmlBuilder>(sp => new ParametersPresenterXmlBuilder("params.xml"))
                .AddSingleton<IParameterVMsHolder, ParameterVMsHolder>()

                // Adding models.
                .AddSingleton<IDeviceConnectionModel, DeviceConnectionModel>()

                // Adding ViewModels.
                .AddSingleton<IDeviceConnectionViewModel, DeviceConnectionViewModel>()
                .AddSingleton<IAinsCountSelectionViewModel, AinsCountSelectionViewModel>();
            //.AddSingleton<ITelemetry100ViewModel, Telemetry100ViewModel>();

            return serviceCollection.BuildServiceProvider();
        }
    }


    internal class AvaloniaThreadNotifier : IThreadNotifier
    {
        public void Notify(Action notifyAction)
        {
            Dispatcher.UIThread.InvokeAsync(notifyAction);
        }
    }
}