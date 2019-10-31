using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using ReactiveUI;
using TopDriveSystem.ControlApp.Models.AinsCounter;

namespace TopDriveSystem.ControlApp.ViewModels.AinsCountSelection
{
    public class AinsCountSelectionViewModel : ReactiveObject, IAinsCountSelectionViewModel
    {
        private readonly IAinsCounterRaisable _ainsCounterRaisable;
        private bool _isConnected = false;
        private int _selectedAinsCount;

        public AinsCountSelectionViewModel(IAinsCounterRaisable ainsCounterRaisable)
        {
            _ainsCounterRaisable = ainsCounterRaisable;
            _selectedAinsCount = 1;

            this.WhenAnyValue(x => x.SelectedAinsCount)
                .Subscribe(count => _ainsCounterRaisable.SetAinsCountAndRaiseChange(count));
            // TODO: dispose
        }

        [DataMember]
        public int SelectedAinsCount
        {
            get => _selectedAinsCount;
            set => this.RaiseAndSetIfChanged(ref _selectedAinsCount, value);
        }

        public IReadOnlyList<int> AinsCountList => new List<int> {1, 2, 3};
    }
}