using AlienJust.Support.Mvvm;
using TopDriveSystem.ControlApp.Models.ParamLogger;

namespace TopDriveSystem.ConfigApp.LookedLikeAbb.Parameters.ParameterBooleanReadonly
{
    internal class ParameterBooleanReadonlyViewModel : ViewModelBase, ICheckableParameter
    {
        private readonly IParameterLogger _parameterLogger;

        private bool? _currentValue;
        private bool _isChecked;

        public ParameterBooleanReadonlyViewModel(string name, bool? currentValue, IParameterLogger parameterLogger)
        {
            Name = name;

            _isChecked = false;
            _currentValue = currentValue;
            _parameterLogger = parameterLogger;
        }

        public string Name { get; }

        public bool? CurrentValue
        {
            get => _currentValue;
            set
            {
                if (_currentValue != value)
                {
                    _currentValue = value;
                    RaisePropertyChanged(() => CurrentValue);
                    RaisePropertyChanged(() => FormattedValue);
                }

                if (_isChecked) _parameterLogger.LogDiscreteParameter(Name, value);
            }
        }

        public string FormattedValue => _currentValue.HasValue ? _currentValue.Value ? "1" : "0" : "?";

        public bool IsChecked
        {
            get => _isChecked;
            set
            {
                if (value != _isChecked)
                {
                    _isChecked = value;
                    RaisePropertyChanged(() => IsChecked);
                    if (!_isChecked) _parameterLogger.RemoveSeries(Name);
                }
            }
        }
    }
}