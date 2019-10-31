using AlienJust.Support.Mvvm;

namespace TopDriveSystem.ConfigApp.LookedLikeAbb.Parameters.ParameterDoubleEditCheck
{
    internal class ParameterDoubleEditCheckViewModel : ViewModelBase, ICheckableParameter
    {
        private double? _currentValue;
        private bool _isChecked;

        public ParameterDoubleEditCheckViewModel(string name, string format, double minimumValue, double maximumValue,
            double? currentValue)
        {
            Name = name;
            Format = format;
            MinimumValue = minimumValue;
            MaximumValue = maximumValue;

            _isChecked = false;
            _currentValue = currentValue;
            Increment = 1.0;
        }

        public string Name { get; }
        public string Format { get; }
        public double MinimumValue { get; }
        public double MaximumValue { get; }

        public double Increment { get; set; }


        public double? CurrentValue
        {
            get => _currentValue;
            set
            {
                if (_currentValue != value)
                {
                    _currentValue = value;
                    RaisePropertyChanged(() => CurrentValue);
                }
            }
        }

        public bool IsChecked
        {
            get => _isChecked;
            set
            {
                if (value != _isChecked)
                {
                    _isChecked = value;
                    RaisePropertyChanged(() => IsChecked);
                }
            }
        }
    }
}