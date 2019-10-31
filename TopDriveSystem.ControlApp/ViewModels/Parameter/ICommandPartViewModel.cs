using System;
using System.ComponentModel;

namespace TopDriveSystem.ControlApp.ViewModels.Parameter
{
    public interface ICommandPartViewModel : INotifyPropertyChanged
    {
        string ReceiveTimeText { get; }
        string DataText { get; }
    }
}