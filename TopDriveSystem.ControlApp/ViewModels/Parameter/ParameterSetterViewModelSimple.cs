using System;
using System.Collections.Generic;
using System.Reactive;
using System.Windows.Input;
using AlienJust.Support.Concurrent.Contracts;
using ReactiveUI;
using TopDriveSystem.CommandSenders.Contracts;
using TopDriveSystem.Parameters;

namespace TopDriveSystem.ControlApp.ViewModels.Parameter
{
    internal sealed class ParameterSetterViewModelSimple : ReactiveObject, IParameterSetterViewModel
    {
        private readonly ReactiveCommand<Unit, Unit> _setValue;
        private readonly IThreadNotifier _uiNotifier;

        private LastSetStateResult _lastSet;

        private ParameterPreselectedValue _selectedValue;

        private double _value;

        public ParameterSetterViewModelSimple(IParameterSetter parameterSetter, IThreadNotifier uiNotifier,
            IParameterInjectionConfiguration injectionConfiguration)
        {
            _uiNotifier = uiNotifier;
            CustomValueList = injectionConfiguration.PreselectedValueList;

            _lastSet = LastSetStateResult.Unknown;

            // TODO: follow https://reactiveui.net/docs/guidelines/framework/prefer-oaph-over-properties
            var x = this.WhenAnyValue(vm => vm.SelectedValue).Subscribe(val => Value = val.Value);

            _setValue = ReactiveCommand.Create(() =>
            {
                Console.WriteLine($"Setting for param {injectionConfiguration.ZeroBasedParameterNumber} value " +
                                  injectionConfiguration.GetValue(_value));
                parameterSetter.SetParameterAsync(injectionConfiguration.ZeroBasedParameterNumber,
                    injectionConfiguration.GetValue(_value), ex =>
                    {
                        _uiNotifier.Notify(() =>
                        {
                            if (ex != null) LastSet = LastSetStateResult.Unsuccess;
                            else LastSet = LastSetStateResult.Success;
                        });
                    });
            });
        }

        public ParameterPreselectedValue SelectedValue
        {
            get => _selectedValue;
            set => this.RaiseAndSetIfChanged(ref _selectedValue, value);
        }

        public LastSetStateResult LastSet
        {
            get => _lastSet;
            set => this.RaiseAndSetIfChanged(ref _lastSet, value);
        }

        public IList<ParameterPreselectedValue> CustomValueList { get; }

        public ICommand SetValue => _setValue;

        public double Value
        {
            get => _value;
            set => this.RaiseAndSetIfChanged(ref _value, value);
        }
    }
}