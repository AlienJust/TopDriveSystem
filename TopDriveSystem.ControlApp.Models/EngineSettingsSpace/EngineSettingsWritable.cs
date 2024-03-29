﻿using TopDriveSystem.Commands.EngineSettings;

namespace TopDriveSystem.ControlApp.Models.EngineSettingsSpace
{
    public class EngineSettingsWritable : IEngineSettings
    {
        public EngineSettingsWritable(IEngineSettings settings)
        {
            Inom = settings.Inom;
            Nnom = settings.Nnom;
            Nmax = settings.Nmax;
            Pnom = settings.Pnom;
            CosFi = settings.CosFi;
            Eff = settings.Eff;
            Mass = settings.Mass;
            MmM = settings.MmM;
            Height = settings.Height;
            I2Tmax = settings.I2Tmax;
            Icontinious = settings.Icontinious;
            ZeroF = settings.ZeroF;
        }

        public ushort Inom { get; set; }
        public ushort Nnom { get; set; }
        public ushort Nmax { get; set; }
        public decimal Pnom { get; set; }
        public decimal CosFi { get; set; }
        public decimal Eff { get; set; }
        public ushort Mass { get; set; }
        public ushort MmM { get; set; }
        public ushort Height { get; set; }

        public uint I2Tmax { get; set; }
        public ushort Icontinious { get; set; }
        public ushort ZeroF { get; set; }

        public void ModifyFromPart(IEngineSettingsPart settings)
        {
            if (settings.Inom.HasValue) Inom = settings.Inom.Value;
            if (settings.Nnom.HasValue) Nnom = settings.Nnom.Value;
            if (settings.Nmax.HasValue) Nmax = settings.Nmax.Value;
            if (settings.Pnom.HasValue) Pnom = settings.Pnom.Value;
            if (settings.CosFi.HasValue) CosFi = settings.CosFi.Value;
            if (settings.Eff.HasValue) Eff = settings.Eff.Value;
            if (settings.Mass.HasValue) Mass = settings.Mass.Value;
            if (settings.MmM.HasValue) MmM = settings.MmM.Value;
            if (settings.Height.HasValue) Height = settings.Height.Value;
            if (settings.I2Tmax.HasValue) I2Tmax = settings.I2Tmax.Value;
            if (settings.Icontinious.HasValue) Icontinious = settings.Icontinious.Value;
            if (settings.ZeroF.HasValue) ZeroF = settings.ZeroF.Value;
        }
    }
}