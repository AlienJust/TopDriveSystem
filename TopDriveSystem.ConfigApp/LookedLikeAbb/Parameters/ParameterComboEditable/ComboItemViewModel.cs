﻿using AlienJust.Support.Mvvm;

namespace TopDriveSystem.ConfigApp.LookedLikeAbb.Parameters.ParameterComboEditable
{
    internal class ComboItemViewModel<TModel> : ViewModelBase
    {
        public string ComboText { get; set; }
        public TModel ComboValue { get; set; }
    }
}