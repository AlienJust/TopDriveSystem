using AlienJust.Support.Mvvm;

namespace TopDriveSystem.ConfigApp.LookedLikeAbb.Parameters.ParameterBooleanEditCheck
{
    internal class ParameterBooleanEditCheckViewModel : ViewModelBase, ICheckableParameter
    {
        private bool _isChecked;
        private bool? _value;

        public ParameterBooleanEditCheckViewModel(string name, string offText, string onText)
        {
            Name = name;
            OffText = offText;
            OnText = onText;

            _isChecked = false;
        }

        public string Name { get; }
        public string OffText { get; }
        public string OnText { get; }


        public bool? Value
        {
            get => _value;
            set
            {
                if (_value != value)
                {
                    _value = value;
                    RaisePropertyChanged(() => Value);
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