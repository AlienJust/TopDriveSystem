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
        private readonly IPsnProtocolCommandPartConfiguration _config;

        private readonly ICommandPartListener _listener;
        private readonly IThreadNotifier _uiNotifier;

        private string _dataText;

        private string _receivedTimeText;

        public CommandPartViewModelSimple(ICommandPartListener listener, IThreadNotifier uiNotifier,
            IPsnProtocolCommandPartConfiguration config)
        {
            _listener = listener;
            _uiNotifier = uiNotifier;
            _config = config;

            _listener.CommandPartReceived += ListenerValueReceived;
            _dataText = "?";
            ReceiveTimeText = "?";
        }

        public string Name { get; }

        public string DataText
        {
            get => _dataText;
            set => this.RaiseAndSetIfChanged(ref _dataText, value);
        }

        public string ReceiveTimeText
        {
            get => _receivedTimeText;
            set => this.RaiseAndSetIfChanged(ref _receivedTimeText, value);
        }

        private void ListenerValueReceived(object sender, CommandPartReceivedEventArgs e)
        {
            if (e.Data.CmdPartConfig.Id.IdentyString == _config.Id.IdentyString)
                _uiNotifier.Notify(() =>
                {
                    DataText = e.Data.DataBytes.ToText();
                    ReceiveTimeText = DateTime.Now.ToString("HH:mm:ss");
                });
        }
    }
}