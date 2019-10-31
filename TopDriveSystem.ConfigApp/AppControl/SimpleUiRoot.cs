using AlienJust.Support.Concurrent.Contracts;

namespace TopDriveSystem.ConfigApp.AppControl {
	class SimpleUiRoot : IUserInterfaceRoot {
		public SimpleUiRoot(IThreadNotifier notifier) {
			Notifier = notifier;
		}

		public IThreadNotifier Notifier { get; }
	}
}