﻿using AlienJust.Support.Loggers.Contracts;
using TopDriveSystem.ConfigApp.AppControl.AinsCounter;
using TopDriveSystem.ConfigApp.AppControl.CommandSenderHost;
using TopDriveSystem.ConfigApp.AppControl.Cycle;
using TopDriveSystem.ConfigApp.AppControl.LoggerHost;
using TopDriveSystem.ConfigApp.AppControl.NotifySendingEnabled;
using TopDriveSystem.ConfigApp.AppControl.ParamLogger;
using TopDriveSystem.ConfigApp.AppControl.TargetAddressHost;
using TopDriveSystem.ConfigApp.LookedLikeAbb;
using TopDriveSystem.ConfigApp.LookedLikeAbb.Group07Parameters;
using TopDriveSystem.ConfigApp.LookedLikeAbb.Group08Parameters;
using TopDriveSystem.ConfigApp.LookedLikeAbb.Group09Parameters;

namespace TopDriveSystem.ConfigApp.NewLook.Telemetry {
	class TelemetryViewModel {
		public Group01ParametersViewModel Group01ParametersVm { get; }
		public Group02ParametersViewModel Group02ParametersVm { get; }
		public Group03ParametersViewModel Group03ParametersVm { get; }
		public Group04ParametersViewModel Group04ParametersVm { get; }
		public Group07ParametersViewModel Group07ParametersVm { get; }
		public Group08ParametersViewModel Group08ParametersVm { get; }
		public Group09ParametersViewModel Group09ParametersVm { get; }

		public TelemetryViewModel(IUserInterfaceRoot userInterfaceRoot, ICommandSenderHost commanSenderHost, ITargetAddressHost targetAddressHost, ILogger logger, ICycleThreadHolder cycleThreadHolder, IAinsCounter ainsCounter, IParameterLogger parameterLogger, INotifySendingEnabled notifySendingEnabled) {
			Group01ParametersVm = new Group01ParametersViewModel(commanSenderHost, targetAddressHost, userInterfaceRoot, logger, ainsCounter, parameterLogger);
			cycleThreadHolder.RegisterAsCyclePart(Group01ParametersVm);

			Group02ParametersVm = new Group02ParametersViewModel(commanSenderHost, targetAddressHost, userInterfaceRoot, logger, parameterLogger);
			cycleThreadHolder.RegisterAsCyclePart(Group02ParametersVm);

			Group03ParametersVm = new Group03ParametersViewModel(commanSenderHost, targetAddressHost, userInterfaceRoot, logger, parameterLogger);
			cycleThreadHolder.RegisterAsCyclePart(Group03ParametersVm);

			Group04ParametersVm = new Group04ParametersViewModel(commanSenderHost, targetAddressHost, userInterfaceRoot, logger, notifySendingEnabled);

			Group07ParametersVm = new Group07ParametersViewModel(commanSenderHost, targetAddressHost, userInterfaceRoot, logger, parameterLogger);
			cycleThreadHolder.RegisterAsCyclePart(Group07ParametersVm);

			Group08ParametersVm = new Group08ParametersViewModel(commanSenderHost, targetAddressHost, userInterfaceRoot, logger, parameterLogger);
			cycleThreadHolder.RegisterAsCyclePart(Group08ParametersVm);

			Group09ParametersVm = new Group09ParametersViewModel(commanSenderHost, targetAddressHost, userInterfaceRoot, logger, ainsCounter, parameterLogger);
			cycleThreadHolder.RegisterAsCyclePart(Group09ParametersVm);
		}
	}
}
