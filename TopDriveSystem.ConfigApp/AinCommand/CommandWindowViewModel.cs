using AlienJust.Support.Mvvm;

namespace TopDriveSystem.ConfigApp.AinCommand
{
    internal class CommandWindowViewModel : ViewModelBase
    {
        private bool _isTopMost;

        public CommandWindowViewModel(AinCommandAndCommonTelemetryViewModel ainCommandOnlyVm)
        {
            AinCommandViewVm = ainCommandOnlyVm;
            _isTopMost = false;
        }

        public AinCommandAndCommonTelemetryViewModel AinCommandViewVm { get; }

        public bool IsTopMost
        {
            get => _isTopMost;
            set
            {
                if (_isTopMost != value)
                {
                    _isTopMost = value;
                    RaisePropertyChanged(() => IsTopMost);
                }
            }
        }
    }
}