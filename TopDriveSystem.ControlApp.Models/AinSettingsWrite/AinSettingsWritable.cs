using AlienJust.Support.Collections;
using TopDriveSystem.Commands.AinSettings;

namespace TopDriveSystem.ControlApp.Models.AinSettingsWrite
{
    public sealed class AinSettingsWritable : IAinSettings
    {
        public AinSettingsWritable(IAinSettings settings)
        {
            KpW = settings.KpW;
            KiW = settings.KiW;

            FiNom = settings.FiNom;
            Imax = settings.Imax;
            UdcMax = settings.UdcMax;
            UdcMin = settings.UdcMin;
            Fnom = settings.Fnom;
            Fmax = settings.Fmax;
            DflLim = settings.DflLim;
            FlMinMin = settings.FlMinMin;
            IoutMax = settings.IoutMax;
            FiMin = settings.FiMin;
            DacCh = settings.DacCh;
            Imcw = settings.Imcw;
            Ia0 = settings.Ia0;
            Ib0 = settings.Ib0;
            Ic0 = settings.Ic0;
            Udc0 = settings.Udc0;
            TauR = settings.TauR;
            Lm = settings.Lm;
            Lsl = settings.Lsl;
            Lrl = settings.Lrl;

            KpFi = settings.KpFi;
            KiFi = settings.KiFi;
            KpId = settings.KpId;
            KiId = settings.KiId;
            KpIq = settings.KpIq;
            KiIq = settings.KiIq;
            AccDfDt = settings.AccDfDt;
            DecDfDt = settings.DecDfDt;
            Unom = settings.Unom;
            TauFlLim = settings.TauFlLim;
            Rs = settings.Rs;
            Fmin = settings.Fmin;
            TauM = settings.TauM;
            TauF = settings.TauF;
            TauFSet = settings.TauFSet;
            TauFi = settings.TauFi;
            IdSetMin = settings.IdSetMin;
            IdSetMax = settings.IdSetMax;
            UchMin = settings.UchMin;
            UchMax = settings.UchMax;

            Np = settings.Np;
            NimpFloorCode = settings.NimpFloorCode;
            FanMode = settings.FanMode;
            DirectCurrentMagnetization = settings.DirectCurrentMagnetization;

            UmodThr = settings.UmodThr;
            EmdecDfdt = settings.EmdecDfdt;
            TextMax = settings.TextMax;
            ToHl = settings.ToHl;

            Ain1LinkFault = settings.Ain1LinkFault;
            Ain2LinkFault = settings.Ain2LinkFault;
            Ain3LinkFault = settings.Ain3LinkFault;
        }

        public BytesPair Reserved00 { get; set; }

        public decimal KpW { get; set; }
        public decimal KiW { get; set; }

        public decimal FiNom { get; set; }
        public short Imax { get; set; }
        public short UdcMax { get; set; }
        public short UdcMin { get; set; }
        public decimal Fnom { get; set; }
        public decimal Fmax { get; set; }
        public decimal DflLim { get; set; }
        public decimal FlMinMin { get; set; }
        public short IoutMax { get; set; }
        public decimal FiMin { get; set; }
        public ushort DacCh { get; set; }
        public ushort Imcw { get; set; }
        public short Ia0 { get; set; }
        public short Ib0 { get; set; }
        public short Ic0 { get; set; }
        public short Udc0 { get; set; }

        public decimal TauR { get; set; }
        public decimal Lm { get; set; }
        public decimal Lsl { get; set; }
        public decimal Lrl { get; set; }

        public BytesPair Reserved24 { get; set; }
        public decimal KpFi { get; set; }
        public decimal KiFi { get; set; }
        public BytesPair Reserved28 { get; set; }

        public decimal KpId { get; set; }

        public decimal KiId { get; set; }
        public BytesPair Reserved32 { get; set; }

        public decimal KpIq { get; set; }
        public decimal KiIq { get; set; }

        public decimal AccDfDt { get; set; }
        public decimal DecDfDt { get; set; }
        public decimal Unom { get; set; }
        public decimal TauFlLim { get; set; }
        public decimal Rs { get; set; }
        public decimal Fmin { get; set; }

        public decimal TauM { get; set; }
        public decimal TauF { get; set; }
        public decimal TauFSet { get; set; }
        public decimal TauFi { get; set; }

        public short IdSetMin { get; set; }
        public short IdSetMax { get; set; }

        public BytesPair UchMin { get; set; }
        public BytesPair UchMax { get; set; }

        public BytesPair Reserved50 { get; set; }
        public BytesPair Reserved51 { get; set; }

        public int Np { get; set; }
        public int NimpFloorCode { get; set; }
        public AinTelemetryFanWorkmode FanMode { get; set; }
        public bool DirectCurrentMagnetization { get; set; }

        public decimal UmodThr { get; set; }
        public decimal EmdecDfdt { get; set; }
        public short TextMax { get; set; }
        public short ToHl { get; set; }

        public bool Ain1LinkFault { get; set; }
        public bool Ain2LinkFault { get; set; }
        public bool Ain3LinkFault { get; set; }

        public void ModifyFromPart(IAinSettingsPart part)
        {
            if (part.KpW.HasValue) KpW = part.KpW.Value;
            if (part.KiW.HasValue) KiW = part.KiW.Value;

            if (part.FiNom.HasValue) FiNom = part.FiNom.Value;
            if (part.Imax.HasValue) Imax = part.Imax.Value;
            if (part.UdcMax.HasValue) UdcMax = part.UdcMax.Value;
            if (part.UdcMin.HasValue) UdcMin = part.UdcMin.Value;
            if (part.Fnom.HasValue) Fnom = part.Fnom.Value;
            if (part.Fmax.HasValue) Fmax = part.Fmax.Value;
            if (part.DflLim.HasValue) DflLim = part.DflLim.Value;
            if (part.FlMinMin.HasValue) FlMinMin = part.FlMinMin.Value;
            if (part.IoutMax.HasValue) IoutMax = part.IoutMax.Value;
            if (part.FiMin.HasValue) FiMin = part.FiMin.Value;
            if (part.DacCh.HasValue) DacCh = part.DacCh.Value;
            if (part.Imcw.HasValue) Imcw = part.Imcw.Value;
            if (part.Ia0.HasValue) Ia0 = part.Ia0.Value;
            if (part.Ib0.HasValue) Ib0 = part.Ib0.Value;
            if (part.Ic0.HasValue) Ic0 = part.Ic0.Value;
            if (part.Udc0.HasValue) Udc0 = part.Udc0.Value;
            if (part.TauR.HasValue) TauR = part.TauR.Value;
            if (part.Lm.HasValue) Lm = part.Lm.Value;
            if (part.Lsl.HasValue) Lsl = part.Lsl.Value;
            if (part.Lrl.HasValue) Lrl = part.Lrl.Value;

            if (part.KpFi.HasValue) KpFi = part.KpFi.Value;
            if (part.KiFi.HasValue) KiFi = part.KiFi.Value;
            if (part.KpId.HasValue) KpId = part.KpId.Value;
            if (part.KiId.HasValue) KiId = part.KiId.Value;
            if (part.KpIq.HasValue) KpIq = part.KpIq.Value;
            if (part.KiIq.HasValue) KiIq = part.KiIq.Value;
            if (part.AccDfDt.HasValue) AccDfDt = part.AccDfDt.Value;
            if (part.DecDfDt.HasValue) DecDfDt = part.DecDfDt.Value;
            if (part.Unom.HasValue) Unom = part.Unom.Value;
            if (part.TauFlLim.HasValue) TauFlLim = part.TauFlLim.Value;
            if (part.Rs.HasValue) Rs = part.Rs.Value;
            if (part.Fmin.HasValue) Fmin = part.Fmin.Value;
            if (part.TauM.HasValue) TauM = part.TauM.Value;
            if (part.TauF.HasValue) TauF = part.TauF.Value;
            if (part.TauFSet.HasValue) TauFSet = part.TauFSet.Value;
            if (part.TauFi.HasValue) TauFi = part.TauFi.Value;
            if (part.IdSetMin.HasValue) IdSetMin = part.IdSetMin.Value;
            if (part.IdSetMax.HasValue) IdSetMax = part.IdSetMax.Value;
            if (part.UchMin.HasValue) UchMin = part.UchMin.Value;
            if (part.UchMax.HasValue) UchMax = part.UchMax.Value;

            if (part.Np.HasValue) Np = part.Np.Value;
            if (part.NimpFloorCode.HasValue) NimpFloorCode = part.NimpFloorCode.Value;
            if (part.FanMode.HasValue) FanMode = part.FanMode.Value;
            if (part.DirectCurrentMagnetization.HasValue)
                DirectCurrentMagnetization = part.DirectCurrentMagnetization.Value;

            if (part.UmodThr.HasValue) UmodThr = part.UmodThr.Value;
            if (part.EmdecDfdt.HasValue) EmdecDfdt = part.EmdecDfdt.Value;
            if (part.TextMax.HasValue) TextMax = part.TextMax.Value;
            if (part.ToHl.HasValue) ToHl = part.ToHl.Value;

            if (part.Ain1LinkFault.HasValue) Ain1LinkFault = part.Ain1LinkFault.Value;
            if (part.Ain2LinkFault.HasValue) Ain2LinkFault = part.Ain2LinkFault.Value;
            if (part.Ain3LinkFault.HasValue) Ain3LinkFault = part.Ain3LinkFault.Value;
        }
    }
}