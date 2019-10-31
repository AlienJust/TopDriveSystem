using System.Collections.Generic;
using AlienJust.Support.Loggers.Contracts;

namespace TopDriveSystem.ConfigApp.AppControl.LoggerHost
{
    internal class LoggerRegistrationPointThreadSafe : ILoggerRegistrationPoint, ILogger
    {
        private readonly List<ILogger> _registredLoggers;
        private readonly object _syncLoggers;

        public LoggerRegistrationPointThreadSafe()
        {
            _registredLoggers = new List<ILogger>();
            _syncLoggers = new object();
        }

        public void Log(object obj)
        {
            lock (_syncLoggers)
            {
                foreach (var logger in _registredLoggers) logger.Log(obj);
            }
        }

        public void RegisterLoggegr(ILogger logger)
        {
            lock (_syncLoggers)
            {
                _registredLoggers.Add(logger);
            }
        }
    }
}