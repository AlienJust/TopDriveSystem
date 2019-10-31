using System;
using AlienJust.Support.Concurrent.Contracts;
using AlienJust.Support.Text;
using DataAbstractionLevel.Low.PsnConfig.Contracts;
using ReactiveUI;
using TopDriveSystem.Model.Listening;

namespace TopDriveSystem.ControlApp.ViewModels.Parameter
{
    internal sealed class CommandPartViewModelSimple : ReactiveObject, ICommandPartViewModel
    {

        private readonly ICommandPartListener _listener;
        private readonly IThreadNotifier _uiNotifier;

        public string Name { get; }

        private readonly IPsnProtocolCommandPartConfiguration _config;

        private string _dataText;
        public string DataText
        {
            get => _dataText;
            set => this.RaiseAndSetIfChanged(ref _dataText, value);
        }

        private string _receivedTimeText;
        public string ReceiveTimeText
        {
            get => _receivedTimeText;
            set => this.RaiseAndSetIfChanged(ref _receivedTimeText, value);
        }

        public CommandPartViewModelSimple(ICommandPartListener listener, IThreadNotifier uiNotifier, IPsnProtocolCommandPartConfiguration config)
        {
            _listener = listener;
            _uiNotifier = uiNotifier;
            _config = config;

            _listener.CommandPartReceived += ListenerValueReceived;
            _dataText = "?";
            ReceiveTimeText = "?";
        }

        private void ListenerValueReceived(object sender, CommandPartReceivedEventArgs e)
        {
            if (e.Data.CmdPartConfig.Id.IdentyString == _config.Id.IdentyString)
            {
                _uiNotifier.Notify(() =>
                {

                    DataText = e.Data.DataBytes.ToText();
                    ReceiveTimeText = DateTime.Now.ToString("HH:mm:ss");
                });
            }
        }
    }
}