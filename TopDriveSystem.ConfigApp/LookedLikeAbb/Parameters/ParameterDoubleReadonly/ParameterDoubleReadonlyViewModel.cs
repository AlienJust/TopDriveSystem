using AlienJust.Support.Mvvm;
using TopDriveSystem.ControlApp.Models.ParamLogger;

namespace TopDriveSystem.ConfigApp.LookedLikeAbb.Parameters.ParameterDoubleReadonly
{
    internal class ParameterDoubleReadonlyViewModel : ViewModelBase, ICheckableParameter
    {
        private readonly IParameterLogger _parameterLogger;
        private double? _currentValue;
        private bool _isChecked;

        public ParameterDoubleReadonlyViewModel(string name, string format, double? currentValue,
            IParameterLogger parameterLogger)
        {
            Name = name;
            Format = format;

            _isChecked = false;
            _currentValue = currentValue;
            _parameterLogger = parameterLogger;
        }

        public string Name { get; }
        public string Format { get; }

        public double? CurrentValue
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

                if (_isChecked) _parameterLogger.LogAnalogueParameter(Name, value);
            }
        }

        public string FormattedValue => _currentValue?.ToString(Format) ?? "-";

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