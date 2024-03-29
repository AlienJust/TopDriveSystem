﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Input;
using AlienJust.Support.Loggers.Contracts;
using AlienJust.Support.Mvvm;
using AlienJust.Support.UI.Contracts;
using TopDriveSystem.Commands.SystemControl;
using TopDriveSystem.ConfigApp.AinTelemetry;
using TopDriveSystem.ControlApp.Models.CommandSenderHost;
using TopDriveSystem.ControlApp.Models.NotifySendingEnabled;
using TopDriveSystem.ControlApp.Models.TargetAddressHost;

namespace TopDriveSystem.ConfigApp.SystemControl
{
    internal class SystemControlViewModel : ViewModelBase, IDebugInformationShower
    {
        private readonly RelayCommand _cmdSetBootloader;
        private readonly ICommandSenderHost _commandSenderHost;
        private readonly ILinkContol _linkControl;
        private readonly ILogger _logger;
        private readonly INotifySendingEnabled _sendingEnabledControl;
        private readonly ITargetAddressHost _targerAddressHost;
        private readonly IUserInterfaceRoot _userInterfaceRoot;
        private readonly IWindowSystem _windowSystem;

        public SystemControlViewModel(ICommandSenderHost commandSenderHost, ITargetAddressHost targerAddressHost,
            IUserInterfaceRoot userInterfaceRoot, ILogger logger, IWindowSystem windowSystem,
            INotifySendingEnabled sendingEnabledControl, ILinkContol linkControl,
            TelemetryCommonViewModel commonTelemetryVm)
        {
            _commandSenderHost = commandSenderHost;
            _targerAddressHost = targerAddressHost;
            _userInterfaceRoot = userInterfaceRoot;
            _logger = logger;
            _windowSystem = windowSystem;
            _sendingEnabledControl = sendingEnabledControl;
            _linkControl = linkControl;

            CommonTelemetryVm = commonTelemetryVm;

            _cmdSetBootloader = new RelayCommand(SetBootloader, () => _sendingEnabledControl.IsSendingEnabled);
            CmdRestart = new RelayCommand(Restart, () => _sendingEnabledControl.IsSendingEnabled);
            CmdFlash = new RelayCommand(Flash, () => _sendingEnabledControl.IsSendingEnabled);

            _sendingEnabledControl.SendingEnabledChanged += SendingEnabledControlOnSendingEnabledChanged;

            DebugParametersTrendVm = new DebugParametersTrendViewModel();
            DebugParametersVm = new DebugParametersViewModel();
        }


        public ICommand CmdSetBootloader => _cmdSetBootloader;

        public RelayCommand CmdRestart { get; }


        public RelayCommand CmdFlash { get; }


        public TelemetryCommonViewModel CommonTelemetryVm { get; }

        public DebugParametersTrendViewModel DebugParametersTrendVm { get; }

        public DebugParametersViewModel DebugParametersVm { get; }

        public void ShowBytes(IList<byte> bytes)
        {
            //todo: notify child wms about ShowBytes()
            DebugParametersTrendVm.ShowBytes(bytes);
            DebugParametersVm.ShowBytes(bytes);
        }

        private void SendingEnabledControlOnSendingEnabledChanged(bool issendingenabled)
        {
            _cmdSetBootloader.RaiseCanExecuteChanged();
            CmdRestart.RaiseCanExecuteChanged();
            CmdFlash.RaiseCanExecuteChanged();
        }

        private void SetBootloader()
        {
            try
            {
                _logger.Log("Переход в режим bootloader");

                var cmd = new SetBootloaderCommand();

                _logger.Log("Команда перехода в режим bootloader поставлена в очередь");
                _commandSenderHost.Sender.SendCommandAsync(
                    _targerAddressHost.TargetAddress
                    , cmd
                    , TimeSpan.FromSeconds(1.0), 1
                    , (exception, bytes) => _userInterfaceRoot.Notifier.Notify(() =>
                    {
                        try
                        {
                            if (exception != null)
                                throw new Exception("Ошибка при передаче данных: " + exception.Message, exception);
                            _logger.Log("Команда перехода в режим bootloader была отправлена");
                        }
                        catch (Exception ex)
                        {
                            _logger.Log(ex.Message);
                        }
                    }));
            }
            catch (Exception ex)
            {
                _logger.Log("Не удалось поставить команду перехода в режим bootloader в очередь: " + ex.Message);
            }
        }


        private void Flash()
        {
            try
            {
                _logger.Log("Переход в режим bootloader");

                var cmd = new SetBootloaderCommand();

                _logger.Log("Команда перехода в режим bootloader поставлена в очередь");
                _commandSenderHost.Sender.SendCommandAsync(
                    _targerAddressHost.TargetAddress
                    , cmd
                    , TimeSpan.FromSeconds(1.0), 1
                    , (exception, bytes) => _userInterfaceRoot.Notifier.Notify(() =>
                    {
                        try
                        {
                            if (exception != null
                            ) //throw new Exception("Ошибка при передаче данных: " + exception.Message, exception);
                                _logger.Log("Произошла ошибка при передаче данных, но это нормально");
                            _logger.Log(
                                "Команда перехода в режим bootloader была отправлена, отключаемся от COM-порта");
                            var psi = new ProcessStartInfo("flash.bat");
                            var process = new Process {StartInfo = psi};
                            process.Start();
                        }
                        catch (Exception ex)
                        {
                            _logger.Log(ex.Message);
                        }
                    }));
                _linkControl.CloseComPort();
            }
            catch (Exception ex)
            {
                _logger.Log("Не удалось поставить команду перехода в режим bootloader в очередь: " + ex.Message);
            }
        }

        private void Restart()
        {
            var cmd = new RestartCommand();
            try
            {
                _logger.Log(cmd.Name);
                _logger.Log("Команда <" + cmd.Name + "> поставлена в очередь");
                _commandSenderHost.Sender.SendCommandAsync(
                    _targerAddressHost.TargetAddress
                    , cmd
                    , TimeSpan.FromSeconds(1), 1
                    , (exception, bytes) => _userInterfaceRoot.Notifier.Notify(() =>
                    {
                        try
                        {
                            if (exception != null)
                                throw new Exception("Ошибка при передаче данных: " + exception.Message, exception);
                            _logger.Log("Команда <" + cmd.Name + "> была отправлена");
                        }
                        catch (Exception ex)
                        {
                            _logger.Log(ex.Message);
                        }
                    }));
            }
            catch (Exception ex)
            {
                _logger.Log("Не удалось поставить команду <" + cmd.Name + "> в очередь: " + ex.Message);
            }
        }
    }
}