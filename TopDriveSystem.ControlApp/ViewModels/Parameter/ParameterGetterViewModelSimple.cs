using AlienJust.Support.Concurrent.Contracts;
using TopDriveSystem.ControlApp.ViewModels.ParameterPresentation;
using ReactiveUI;
using System;
using TopDriveSystem.ConfigApp.AppControl.ParamLogger;
using TopDriveSystem.Model.Listening;

namespace TopDriveSystem.ControlApp.ViewModels.Parameter
{
    internal sealed class ParameterGetterViewModelSimple : ReactiveObject, IParameterGetterViewModel
    {
        private string _value;
        public string Value
        {
            get => _value;
            set => this.RaiseAndSetIfChanged(ref _value, value);
        }

        private bool _isLogged;
        public bool IsLogged
        {
            get => _isLogged;
            set => this.RaiseAndSetIfChanged(ref _isLogged, value);
        }


        private readonly IParamListener _listener;
        private readonly IThreadNotifier _uiNotifier;
        private readonly IParameterView _view;
        private readonly IParameterLogger _parameterLogger;
        
        private readonly string _logName;
        private readonly string _paramId;
        private readonly Action<string, double?> _log;

        public ParameterGetterViewModelSimple(
            string paramId, IParamListener listener, IThreadNotifier uiNotifier,
            IParameterView view, IParameterLogger parameterLogger, bool isBitParam, string logName)
        {
            _listener = listener;
            _uiNotifier = uiNotifier;
            _view = view;
            _parameterLogger = parameterLogger;
            _logName = logName;
            _paramId = paramId;

            _listener.ValueReceived += ListenerValueReceived;
            _log = isBitParam ? (Action<string, double?>)LogBool : _parameterLogger.LogAnalogueParameter;


            _value = "?";
            _isLogged = false;
        }

        /// <summary>
        /// Optimization (choise is made when object is created)
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        void LogBool(string name, double? value)
        {
            _parameterLogger.LogDiscreteParameter(name, value.Value > 0);
        }

        private void ListenerValueReceived(object sender, ParameterValueReceivedEventArgs e)
        {
            if (e.ParameterId == _paramId)
            {
                _uiNotifier.Notify(() =>
                {
                    Value = _view.GetText(e.Value);
                    if (IsLogged) _log(_logName, e.Value);
                });
            }
        }
    }
}