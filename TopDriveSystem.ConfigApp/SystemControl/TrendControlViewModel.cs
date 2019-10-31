using System.Windows.Input;
using AlienJust.Support.Mvvm;

namespace TopDriveSystem.ConfigApp.SystemControl
{
    internal class TrendControlViewModel : ViewModelBase
    {
        private readonly INamedTrendsControl _trendsControl;

        public TrendControlViewModel(string name, INamedTrendsControl trendsControl)
        {
            Name = name;
            _trendsControl = trendsControl;
            ClearTrendCommand = new RelayCommand(ClearTrendData);
        }

        public bool IsTrendVisible
        {
            get => _trendsControl.GetTrendVisibility(Name);
            set
            {
                if (value != IsTrendVisible)
                {
                    _trendsControl.SetTrendVisibility(Name, value);
                    RaisePropertyChanged(() => IsTrendVisible);
                }
            }
        }

        public bool IsSigned
        {
            get => _trendsControl.GetSignedFlag(Name);
            set
            {
                if (value != IsSigned)
                {
                    _trendsControl.SetSignedFlag(Name, value);
                    RaisePropertyChanged(() => IsSigned);
                }
            }
        }

        public ICommand ClearTrendCommand { get; set; }

        public string Name { get; }

        private void ClearTrendData()
        {
            _trendsControl.ClearTrendData(Name);
        }
    }
}