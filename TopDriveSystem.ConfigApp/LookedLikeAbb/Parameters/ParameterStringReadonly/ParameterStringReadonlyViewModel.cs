using AlienJust.Support.Mvvm;

namespace TopDriveSystem.ConfigApp.LookedLikeAbb.Parameters.ParameterStringReadonly
{
    internal class ParameterStringReadonlyViewModel : ViewModelBase
    {
        public ParameterStringReadonlyViewModel(string name, string currentValue)
        {
            Name = name;

            FormattedValue = currentValue;
        }

        public string Name { get; }

        public string CurrentValue
        {
            get => FormattedValue;
            set
            {
                if (FormattedValue != value)
                {
                    FormattedValue = value;
                    RaisePropertyChanged(() => CurrentValue);
                    RaisePropertyChanged(() => FormattedValue);
                }
            }
        }

        public string FormattedValue { get; private set; }
    }
}