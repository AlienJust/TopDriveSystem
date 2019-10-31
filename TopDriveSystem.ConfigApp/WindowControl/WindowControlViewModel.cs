using AlienJust.Support.Mvvm;

namespace TopDriveSystem.ConfigApp.WindowControl
{
    internal class WindowControlViewModel : ViewModelBase
    {
        private bool _isBsEthernetLogWindowShown;
        private bool _isChartWindowShown;
        private bool _isOscilloscopeWindowShown;

        public bool IsBsEthernetLogWindowShown
        {
            get => _isBsEthernetLogWindowShown;
            set
            {
                if (_isBsEthernetLogWindowShown != value)
                {
                    _isBsEthernetLogWindowShown = value;
                    RaisePropertyChanged(() => IsBsEthernetLogWindowShown);
                }
            }
        }

        public bool IsOscilloscopeWindowShown
        {
            get => _isOscilloscopeWindowShown;
            set
            {
                if (_isOscilloscopeWindowShown != value)
                {
                    _isOscilloscopeWindowShown = value;
                    RaisePropertyChanged(() => IsOscilloscopeWindowShown);
                }
            }
        }

        public bool IsChartWindowShown
        {
            get => _isChartWindowShown;
            set
            {
                if (_isChartWindowShown != value)
                {
                    _isChartWindowShown = value;
                    RaisePropertyChanged(() => IsChartWindowShown);
                }
            }
        }
    }
}