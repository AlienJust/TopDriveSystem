﻿using System;

namespace TopDriveSystem.ConfigApp.AppControl.AinSettingsWrite
{
    /// <summary>
    ///     Реализация должна учитывать число АИНов в системе и производить запись соответсвтенно
    /// </summary>
    public interface IAinSettingsWriter
    {
        void WriteSettingsAsync(IAinSettingsPart settings, Action<Exception> callback);
    }
}