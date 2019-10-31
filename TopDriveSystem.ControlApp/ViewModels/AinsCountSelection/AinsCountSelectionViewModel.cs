using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Runtime.Serialization;
using System.Windows.Input;
using TopDriveSystem.ConfigApp.AppControl.AinsCounter;

namespace TopDriveSystem.ControlApp.ViewModels.AinsCountSelection
{
    public class AinsCountSelectionViewModel : ReactiveObject, IAinsCountSelectionViewModel
    {
        private readonly IAinsCounterRaisable _ainsCounterRaisable;
        private int _selectedAinsCount;
        private bool _isConnected = false;
        public AinsCountSelectionViewModel(IAinsCounterRaisable ainsCounterRaisable)
        {
            _ainsCounterRaisable = ainsCounterRaisable;
            _selectedAinsCount = 1;

            this.WhenAnyValue(x => x.SelectedAinsCount).Subscribe(count => _ainsCounterRaisable.SetAinsCountAndRaiseChange(count));
            // TODO: dispose
        }

        [DataMember]
        public int SelectedAinsCount
        {
            get => _selectedAinsCount;
            set => this.RaiseAndSetIfChanged(ref _selectedAinsCount, value);
        }

        public IReadOnlyList<int> AinsCountList => new List<int> { 1, 2, 3 };
    }
}
