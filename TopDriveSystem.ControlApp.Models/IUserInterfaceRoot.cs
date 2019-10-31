using AlienJust.Support.Concurrent.Contracts;

namespace TopDriveSystem.ControlApp.Models
{
    public interface IUserInterfaceRoot
    {
        IThreadNotifier Notifier { get; }
    }
}