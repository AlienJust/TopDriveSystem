﻿using System.Collections.Generic;
using System.Windows.Input;

namespace TopDriveSystem.ConfigApp.NewLook.Archive
{
    internal interface IArchiveViewModel
    {
        string OneBasedArchiveNumber { get; }
        ICommand ReadArchive { get; }
        IList<IArchiveRecordViewModel> ArchiveRecords { get; set; }
    }
}