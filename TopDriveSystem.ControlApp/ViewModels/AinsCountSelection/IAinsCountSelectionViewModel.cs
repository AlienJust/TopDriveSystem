using System.Collections.Generic;
using System.ComponentModel;

namespace TopDriveSystem.ControlApp.ViewModels.AinsCountSelection
{
    public interface IAinsCountSelectionViewModel : INotifyPropertyChanged
    {
        IReadOnlyList<int> AinsCountList { get; }
        int SelectedAinsCount { get; set; }
    }
}