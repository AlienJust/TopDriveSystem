using AlienJust.Support.Concurrent.Contracts;

namespace TopDriveSystem.ConfigApp
{
    public interface IUserInterfaceRoot
    {
        IThreadNotifier Notifier { get; }
    }
}