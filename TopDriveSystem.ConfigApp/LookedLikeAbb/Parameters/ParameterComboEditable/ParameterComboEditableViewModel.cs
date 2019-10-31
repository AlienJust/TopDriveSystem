using System.Collections.Generic;
using AlienJust.Support.Mvvm;

namespace TopDriveSystem.ConfigApp.LookedLikeAbb.Parameters.ParameterComboEditable
{
    internal class ParameterComboEditableViewModel<TComboModel> : ViewModelBase, ICheckableParameter
    {
        private bool _isChecked;
        private ComboItemViewModel<TComboModel> _selectedComboItem;

        public ParameterComboEditableViewModel(string name, IEnumerable<ComboItemViewModel<TComboModel>> comboItems)
        {
            Name = name;
            ComboItems = comboItems;
        }

        public string Name { get; }
        public IEnumerable<ComboItemViewModel<TComboModel>> ComboItems { get; }


        public ComboItemViewModel<TComboModel> SelectedComboItem
        {
            get => _selectedComboItem;
            set
            {
                if (_selectedComboItem != value)
                {
                    _selectedComboItem = value;
                    RaisePropertyChanged(() => SelectedComboItem);
                }
            }
        }

        public bool IsChecked
        {
            get => _isChecked;
            set
            {
                if (value != _isChecked)
                {
                    _isChecked = value;
                    RaisePropertyChanged(() => IsChecked);
                }
            }
        }
    }
}