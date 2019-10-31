using AlienJust.Support.Loggers.Contracts;
using TopDriveSystem.ConfigApp.AppControl.AinsCounter;
using TopDriveSystem.ConfigApp.AppControl.AinSettingsRead;
using TopDriveSystem.ConfigApp.AppControl.AinSettingsStorage;
using TopDriveSystem.ConfigApp.AppControl.CommandSenderHost;
using TopDriveSystem.ConfigApp.AppControl.EngineSettingsSpace;
using TopDriveSystem.ConfigApp.AppControl.NotifySendingEnabled;
using TopDriveSystem.ConfigApp.AppControl.TargetAddressHost;
using TopDriveSystem.ConfigApp.LookedLikeAbb;
using TopDriveSystem.ConfigApp.LookedLikeAbb.AinSettingsRw;
using TopDriveSystem.ConfigApp.LookedLikeAbb.Group106Settings;
using TopDriveSystem.ConfigApp.LookedLikeAbb.Group106Settings.ImvcParameter;
using IAinSettingsReadNotifyRaisable =
    TopDriveSystem.ConfigApp.AppControl.AinSettingsRead.IAinSettingsReadNotifyRaisable;

namespace TopDriveSystem.ConfigApp.NewLook.Settings
{
    internal class SettingsViewModel
    {
        public SettingsViewModel(
            IUserInterfaceRoot userInterfaceRoot,
            ILogger logger,
            IAinSettingsReaderWriter ainSettingsReadedWriter,
            IAinSettingsReadNotify ainSettingsReadNotify,
            IAinSettingsReadNotifyRaisable ainSettingsReadNotifyRaisable,
            IAinSettingsStorage ainSettingsStorage,
            IAinSettingsStorageSettable ainSettingsStorageSettable,
            IAinSettingsStorageUpdatedNotify storageUpdatedNotify,
            IAinsCounter ainsCounter,
            ICommandSenderHost commandSenderHost,
            ITargetAddressHost targetAddressHost,
            INotifySendingEnabled notifySendingEnabled,
            IEngineSettingsReader engineSettingsReader,
            IEngineSettingsWriter engineSettingsWriter,
            IEngineSettingsReadNotify engineSettingsReadNotify,
            IEngineSettingsReadNotifyRaisable engineSettingsReadNotifyRaisable,
            IEngineSettingsStorage engineSettingsStorage,
            IEngineSettingsStorageSettable engineSettingsStorageSettable,
            IEngineSettingsStorageUpdatedNotify engineSettingsStorageUpdatedNotify,
            IMultiLoggerWithStackTrace<int> debugLogger)
        {
            Group20SettingsVm = new Group20SettingsViewModel(userInterfaceRoot, logger, ainSettingsReadedWriter,
                ainSettingsReadNotify, ainSettingsStorage, storageUpdatedNotify, ainsCounter,
                engineSettingsReader, engineSettingsWriter, engineSettingsReadNotify, engineSettingsStorage,
                engineSettingsStorageUpdatedNotify);

            Group22SettingsVm = new Group22SettingsViewModel(userInterfaceRoot, logger, ainSettingsReadedWriter,
                ainSettingsReadNotify, ainSettingsStorage, storageUpdatedNotify, ainsCounter);
            Group23SettingsVm = new Group23SettingsViewModel(userInterfaceRoot, logger, ainSettingsReadedWriter,
                ainSettingsReadNotify, ainSettingsStorage, storageUpdatedNotify, ainsCounter);
            Group24SettingsVm = new Group24SettingsViewModel(userInterfaceRoot, logger, ainSettingsReadedWriter,
                ainSettingsReadNotify, ainSettingsStorage, storageUpdatedNotify, ainsCounter);
            Group25SettingsVm = new Group25SettingsViewModel(userInterfaceRoot, logger, ainSettingsReadedWriter,
                ainSettingsReadNotify, ainSettingsStorage, storageUpdatedNotify, ainsCounter);
            Group26SettingsVm = new Group26SettingsViewModel(userInterfaceRoot, logger, ainSettingsReadedWriter,
                ainSettingsReadNotify, ainSettingsStorage, storageUpdatedNotify, ainsCounter);
            Group27SettingsVm = new Group27SettingsViewModel(userInterfaceRoot, logger, ainSettingsReadedWriter,
                ainSettingsReadNotify, ainSettingsStorage, storageUpdatedNotify, ainsCounter);

            var imcwParamViewModel = new ImcwParameterViewModel();

            Group99SettingsVm = new Group99SettingsViewModel(userInterfaceRoot, logger, ainSettingsReadedWriter,
                ainSettingsReadNotify, ainSettingsStorage, storageUpdatedNotify, ainsCounter,
                engineSettingsReader, engineSettingsWriter, engineSettingsReadNotify, engineSettingsStorage,
                engineSettingsStorageUpdatedNotify,
                imcwParamViewModel);

            Group100SettingsVm = new Group100SettingsViewModel(userInterfaceRoot, logger, ainSettingsReadedWriter,
                ainSettingsReadNotify, ainSettingsStorage, storageUpdatedNotify, ainsCounter);
            Group101SettingsVm = new Group101SettingsViewModel(userInterfaceRoot, logger, ainSettingsReadedWriter,
                ainSettingsReadNotify, ainSettingsStorage, storageUpdatedNotify, ainsCounter);
            Group102SettingsVm = new Group102SettingsViewModel(userInterfaceRoot, logger, ainSettingsReadedWriter,
                ainSettingsReadNotify, ainSettingsStorage, storageUpdatedNotify, ainsCounter);
            Group103SettingsVm = new Group103SettingsViewModel(userInterfaceRoot, logger, ainSettingsReadedWriter,
                ainSettingsReadNotify, ainSettingsStorage, storageUpdatedNotify, ainsCounter);
            //Group104SettingsVm = new Group104SettingsViewModel(userInterfaceRoot, logger, ainSettingsReadedWriter, ainSettingsReadNotify, ainSettingsStorage, storageUpdatedNotify, ainsCounter);
            Group105SettingsVm = new Group105SettingsViewModel(userInterfaceRoot, logger,
                ainSettingsReadedWriter, /*ainSettingsReadNotify, */ainSettingsStorage, storageUpdatedNotify,
                ainsCounter);
            Group106SettingsVm = new Group106SettingsViewModel(userInterfaceRoot, logger, ainSettingsReadedWriter,
                ainSettingsReadNotify, ainSettingsStorage, storageUpdatedNotify, ainsCounter, imcwParamViewModel);
            Group107SettingsVm = new Group107SettingsViewModel(userInterfaceRoot, logger,
                ainSettingsReadedWriter, /*ainSettingsReadNotify, */ainSettingsStorage, storageUpdatedNotify,
                ainsCounter);

            ImportExportVm = new ImportExportViewModel(ainSettingsStorageSettable, ainSettingsReadNotifyRaisable,
                engineSettingsStorageSettable, engineSettingsReadNotifyRaisable, debugLogger);
        }

        public Group20SettingsViewModel Group20SettingsVm { get; }
        public Group22SettingsViewModel Group22SettingsVm { get; }
        public Group23SettingsViewModel Group23SettingsVm { get; }
        public Group24SettingsViewModel Group24SettingsVm { get; }
        public Group25SettingsViewModel Group25SettingsVm { get; }
        public Group26SettingsViewModel Group26SettingsVm { get; }
        public Group27SettingsViewModel Group27SettingsVm { get; }

        public Group99SettingsViewModel Group99SettingsVm { get; }
        public Group100SettingsViewModel Group100SettingsVm { get; }
        public Group101SettingsViewModel Group101SettingsVm { get; }
        public Group102SettingsViewModel Group102SettingsVm { get; }

        public Group103SettingsViewModel Group103SettingsVm { get; }

        //public Group104SettingsViewModel Group104SettingsVm { get; }
        public Group105SettingsViewModel Group105SettingsVm { get; }
        public Group106SettingsViewModel Group106SettingsVm { get; }
        public Group107SettingsViewModel Group107SettingsVm { get; }

        public ImportExportViewModel ImportExportVm { get; set; }
    }
}