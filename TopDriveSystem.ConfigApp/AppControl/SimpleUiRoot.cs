using AlienJust.Support.Concurrent.Contracts;

namespace TopDriveSystem.ConfigApp.AppControl
{
    internal class SimpleUiRoot : IUserInterfaceRoot
    {
        public SimpleUiRoot(IThreadNotifier notifier)
        {
            Notifier = notifier;
        }

        public IThreadNotifier Notifier { get; }
    }
}