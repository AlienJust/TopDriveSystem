using AlienJust.Support.Mvvm;

namespace TopDriveSystem.ConfigApp.LookedLikeAbb.Parameters.ParameterHexadecimalEditable
{
    internal class ParameterHexadecimalEditableViewModel : ViewModelBase, ICheckableParameter
    {
        private int? _currentValue;
        private bool _isChecked;

        public ParameterHexadecimalEditableViewModel(string name, string format, double minimumValue,
            double maximumValue, int? currentValue)
        {
            Name = name;
            Format = format;
            MinimumValue = minimumValue;
            MaximumValue = maximumValue;

            _isChecked = false;
            _currentValue = currentValue;
        }

        public string Name { get; }
        public string Format { get; }
        public double MinimumValue { get; }
        public double MaximumValue { get; }


        public int? CurrentValue
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