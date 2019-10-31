using System;
using System.Collections.Generic;
using System.Reactive;
using System.Windows.Input;
using AlienJust.Support.Concurrent.Contracts;
using TopDriveSystem.ControlApp.ViewModels.ParameterPresentation;
using ReactiveUI;
using TopDriveSystem.CommandSenders.Contracts;

namespace TopDriveSystem.ControlApp.ViewModels.Parameter
{
    internal sealed class ParameterSetterViewModelSimple : ReactiveObject, IParameterSetterViewModel
    {
        public IList<ParameterPreselectedValue> CustomValueList { get; }

        private ParameterPreselectedValue _selectedValue;
        public ParameterPreselectedValue SelectedValue
        {
            get => _selectedValue;
            set => this.RaiseAndSetIfChanged(ref _selectedValue, value);
        }

        private readonly ReactiveCommand<Unit, Unit> _setValue;
        private readonly IThreadNotifier _uiNotifier;

        public ICommand SetValue { get => _setValue; }

        private double _value;
        public double Value
        {
            get => _value;
            set => this.RaiseAndSetIfChanged(ref _value, value);
        }

        private LastSetStateResult _lastSet;
        public LastSetStateResult LastSet
        {
            get => _lastSet; set => this.RaiseAndSetIfChanged(ref _lastSet, value);
        }

        public ParameterSetterViewModelSimple(IParameterSetter parameterSetter, IThreadNotifier uiNotifier, IParameterInjectionConfiguration injectionConfiguration)
        {
            _uiNotifier = uiNotifier;
            CustomValueList = injectionConfiguration.PreselectedValueList;

            _lastSet = LastSetStateResult.Unknown;

            // TODO: follow https://reactiveui.net/docs/guidelines/framework/prefer-oaph-over-properties
            var x = this.WhenAnyValue(vm => vm.SelectedValue).Subscribe(val => Value = val.Value);

            _setValue = ReactiveCommand.Create(() =>
            {
                Console.WriteLine($"Setting for param {injectionConfiguration.ZeroBasedParameterNumber} value " + injectionConfiguration.GetValue(_value));
                parameterSetter.SetParameterAsync(injectionConfiguration.ZeroBasedParameterNumber, injectionConfiguration.GetValue(_value), ex =>
                {
                    _uiNotifier.Notify(() =>
                    {
                        if (ex != null) LastSet = LastSetStateResult.Unsuccess;
                        else LastSet = LastSetStateResult.Success;
                    });
                });
            });
        }
    }
}