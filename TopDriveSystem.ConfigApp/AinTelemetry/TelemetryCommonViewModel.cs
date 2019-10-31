using System.Globalization;
using AlienJust.Support.Mvvm;
using TopDriveSystem.Commands.AinTelemetry;

namespace TopDriveSystem.ConfigApp.AinTelemetry
{
    internal class TelemetryCommonViewModel : ViewModelBase, ICommonAinTelemetryVm
    {
        private const string UnknownValueText = "Неизвестно";
        private string _ainsLinkState;
        private string _ainStatuses;
        private string _engineState;
        private string _faultState;

        public string AinStatuses
        {
            get => _ainStatuses;
            set
            {
                if (_ainStatuses != value)
                {
                    _ainStatuses = value;
                    RaisePropertyChanged(() => AinStatuses);
                }
            }
        }

        public string CommonEngineState
        {
            get => _engineState;
            set
            {
                if (_engineState != value)
                {
                    _engineState = value;
                    RaisePropertyChanged(() => CommonEngineState);
                }
            }
        }

        public string CommonFaultState
        {
            get => _faultState;
            set
            {
                if (_faultState != value)
                {
                    _faultState = value;
                    RaisePropertyChanged(() => CommonFaultState);
                }
            }
        }

        public string AinsLinkState
        {
            get => _ainsLinkState;
            set
            {
                if (_ainsLinkState != value)
                {
                    _ainsLinkState = value;
                    RaisePropertyChanged(() => AinsLinkState);
                }
            }
        }

        public void UpdateCommonEngineState(ushort? value)
        {
            if (!value.HasValue)
            {
                CommonEngineState = UnknownValueText;
            }
            else
            {
                var commonEngineState = value.Value.ToString(CultureInfo.InvariantCulture);
                try
                {
                    commonEngineState += " - " + EngineStateExtensions.GetStateFromUshort(value.Value).ToText();
                }
                catch
                {
                }

                CommonEngineState = commonEngineState;
            }
        }

        public void UpdateCommonFaultState(ushort? value)
        {
            if (!value.HasValue)
            {
                CommonFaultState = UnknownValueText;
            }
            else
            {
                var commonFaultState = value.Value.ToString(CultureInfo.InvariantCulture);
                try
                {
                    commonFaultState += " - " + FaultStateExtensions.GetStateFromUshort(value.Value).ToText();
                }
                catch
                {
                }

                CommonFaultState = commonFaultState;
            }
        }

        public void UpdateAinsLinkState(bool? ain1LinkFault, bool? ain2LinkFault, bool? ain3LinkFault)
        {
            var ain1LinkInfo = ain1LinkFault.HasValue ? ain1LinkFault.Value ? "ER" : "OK" : "X3";
            var ain2LinkInfo = ain2LinkFault.HasValue ? ain2LinkFault.Value ? "ER" : "OK" : "X3";
            var ain3LinkInfo = ain3LinkFault.HasValue ? ain3LinkFault.Value ? "ER" : "OK" : "X3";

            AinsLinkState = ain1LinkInfo + " | " + ain2LinkInfo + " | " + ain3LinkInfo;
        }

        public void UpdateAinStatuses(ushort? status1, ushort? status2, ushort? status3)
        {
            string ainStatuses;
            if (!status1.HasValue)
                ainStatuses = UnknownValueText;
            else
                ainStatuses = "0x" + status1.Value.ToString("X4");
            ainStatuses += " | ";
            if (!status2.HasValue)
                ainStatuses += UnknownValueText;
            else
                ainStatuses += "0x" + status2.Value.ToString("X4");
            ainStatuses += " | ";
            if (!status3.HasValue)
                ainStatuses += UnknownValueText;
            else
                ainStatuses += "0x" + status3.Value.ToString("X4");
            AinStatuses = ainStatuses;
        }
    }
}