using AlienJust.Support.Mvvm;

namespace TopDriveSystem.ConfigApp.LookedLikeAbb.Parameters.ParameterDoubleEditCheck
{
    internal class ParameterDecimalEditCheckViewModel : ViewModelBase, ICheckableParameter
    {
        private decimal? _currentValue;
        private bool _isChecked;

        public ParameterDecimalEditCheckViewModel(string name, string format, decimal minimumValue,
            decimal maximumValue)
        {
            Name = name;
            Format = format;
            MinimumValue = minimumValue;
            MaximumValue = maximumValue;

            _isChecked = false;
            _currentValue = null;
            Increment = 1.0m;
        }

        public string Name { get; }
        public string Format { get; }
        public decimal MinimumValue { get; }
        public decimal MaximumValue { get; }

        public decimal Increment { get; set; }


        public decimal? CurrentValue
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