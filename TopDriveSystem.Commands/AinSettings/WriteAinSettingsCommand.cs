﻿using System;
using AlienJust.Support.Collections;
using AlienJust.Support.Numeric.Bits;
using TopDriveSystem.Commands.Contracts;

namespace TopDriveSystem.Commands.AinSettings
{
    public class WriteAinSettingsCommand : IRrModbusCommandWithReply, IRrModbusCommandResultGetter<bool>,
        IRrModbusCommandWithTestReply
    {
        private readonly IAinSettings _settings;
        private readonly byte _zeroBasedAinNumber;

        public WriteAinSettingsCommand(byte zeroBasedAinNumber, IAinSettings settings)
        {
            _zeroBasedAinNumber = zeroBasedAinNumber;
            _settings = settings;
        }

        private byte OneBasedAinNumber => (byte) (_zeroBasedAinNumber + 1);

        public bool GetResult(byte[] reply)
        {
            if (reply[0] != OneBasedAinNumber)
                throw new Exception("неверный номер АИН в ответе, ожидался " + OneBasedAinNumber);
            return true;
        }

        public byte CommandCode => 0x8E;

        public string Name => "Запись настроек АИН #" + (_zeroBasedAinNumber + 1);

        public byte[] Serialize()
        {
            var settingsSerialized = new byte[114];
            settingsSerialized[0] = _settings.Reserved00.First;
            settingsSerialized[1] = _settings.Reserved00.Second;
            Console.WriteLine("_settings.KpW = " + _settings.KpW.ToString("f10"));
            var bpKpW = BytesPairToDecimalQ8Converter.ConvertToBytesPairQ8(_settings.KpW);
            settingsSerialized[2] = bpKpW.First;
            settingsSerialized[3] = bpKpW.Second;
            Console.WriteLine("_settings.KpW SERIALIZED = " + bpKpW);


            var ushbp52 = (ushort) (_settings.Np | (_settings.NimpFloorCode << 5) |
                                    ((_settings.FanMode.ToIoBits() & 0x03) << 8));
            Console.WriteLine("NPRM without DCM = " + ushbp52.ToString("X4"));
            ushbp52 = _settings.DirectCurrentMagnetization ? ushbp52.SetBit(11) : ushbp52.ResetBit(11);
            var bp52 = BytesPair.FromUnsignedShortLowFirst(ushbp52);
            Console.WriteLine("NPRM = " + bp52);


            settingsSerialized.SerializeIntLowFirst(4, (int) (_settings.KiW * 16777216.0m));
            settingsSerialized.SerializeShortLowFirst(8, (short) (_settings.FiNom * 1000.0m));
            settingsSerialized.SerializeShortLowFirst(10, _settings.Imax);
            settingsSerialized.SerializeShortLowFirst(12, _settings.UdcMax);
            settingsSerialized.SerializeShortLowFirst(14, _settings.UdcMin);

            settingsSerialized.SerializeUshortLowFirst(16, (ushort) (_settings.Fnom * 10.0m));
            settingsSerialized.SerializeUshortLowFirst(18, (ushort) (_settings.Fmax * 10.0m));

            settingsSerialized.SerializeShortLowFirst(20, (short) (_settings.DflLim * 1000.0m));
            settingsSerialized.SerializeShortLowFirst(22, (short) (_settings.FlMinMin * 1000.0m));

            settingsSerialized.SerializeShortLowFirst(24, _settings.IoutMax);
            settingsSerialized.SerializeShortLowFirst(26, (short) (_settings.FiMin * 1000.0m));

            settingsSerialized.SerializeUshortLowFirst(28, _settings.DacCh);
            settingsSerialized.SerializeUshortLowFirst(30, _settings.Imcw);

            settingsSerialized.SerializeShortLowFirst(32, _settings.Ia0);
            settingsSerialized.SerializeShortLowFirst(34, _settings.Ib0);
            settingsSerialized.SerializeShortLowFirst(36, _settings.Ic0);
            settingsSerialized.SerializeShortLowFirst(38, _settings.Udc0);
            settingsSerialized.SerializeShortLowFirst(40, (short) (_settings.TauR * 10000.0m));
            settingsSerialized.SerializeShortLowFirst(42, (short) (_settings.Lm * 100000.0m));
            settingsSerialized.SerializeShortLowFirst(44, (short) (_settings.Lsl * 1000000.0m));
            settingsSerialized.SerializeShortLowFirst(46, (short) (_settings.Lrl * 1000000.0m));

            settingsSerialized[48] = _settings.Reserved24.First;
            settingsSerialized[49] = _settings.Reserved24.Second;

            var bpKpFi = BytesPairToDecimalQ8Converter.ConvertToBytesPairQ8(_settings.KpFi);
            settingsSerialized[50] = bpKpFi.First;
            settingsSerialized[51] = bpKpFi.Second;

            settingsSerialized.SerializeIntLowFirst(52, (int) (_settings.KiFi * 16777216.0m));

            settingsSerialized[56] = _settings.Reserved28.First;
            settingsSerialized[57] = _settings.Reserved28.Second;

            var bpKpId = BytesPairToDecimalQ8Converter.ConvertToBytesPairQ8(_settings.KpId);
            settingsSerialized[58] = bpKpId.First;
            settingsSerialized[59] = bpKpId.Second;

            settingsSerialized.SerializeIntLowFirst(60, (int) (_settings.KiId * 16777216.0m));


            settingsSerialized[64] = _settings.Reserved32.First;
            settingsSerialized[65] = _settings.Reserved32.Second;


            var bpKpIq = BytesPairToDecimalQ8Converter.ConvertToBytesPairQ8(_settings.KpIq);
            settingsSerialized[66] = bpKpIq.First;
            settingsSerialized[67] = bpKpIq.Second;

            settingsSerialized.SerializeIntLowFirst(68, (int) (_settings.KiIq * 16777216.0m));

            settingsSerialized.SerializeShortLowFirst(72, (short) (_settings.AccDfDt * 10.0m));
            settingsSerialized.SerializeShortLowFirst(74, (short) (_settings.DecDfDt * 10.0m));
            settingsSerialized.SerializeUshortLowFirst(76,
                (ushort) Math.Round(_settings.Unom * (decimal) Math.Sqrt(2.0)));

            settingsSerialized.SerializeShortLowFirst(78, (short) (_settings.TauFlLim * 10000.0m));

            settingsSerialized.SerializeUshortLowFirst(80, (ushort) (_settings.Rs * 10000.0m));

            settingsSerialized.SerializeUshortLowFirst(82, (ushort) (_settings.Fmin * 10.0m));
            settingsSerialized.SerializeShortLowFirst(84, (short) (_settings.TauM * 10000.0m));
            settingsSerialized.SerializeShortLowFirst(86, (short) (_settings.TauF * 10000.0m));
            settingsSerialized.SerializeShortLowFirst(88, (short) (_settings.TauFSet * 10000.0m));
            settingsSerialized.SerializeShortLowFirst(90, (short) (_settings.TauFi * 10000.0m));
            settingsSerialized.SerializeShortLowFirst(92, _settings.IdSetMin);
            settingsSerialized.SerializeShortLowFirst(94, _settings.IdSetMax);


            settingsSerialized[96] = _settings.UchMin.First;
            settingsSerialized[97] = _settings.UchMin.Second;

            settingsSerialized[98] = _settings.UchMax.First;
            settingsSerialized[99] = _settings.UchMax.Second;


            settingsSerialized[100] = _settings.Reserved50.First;
            settingsSerialized[101] = _settings.Reserved50.Second;

            settingsSerialized[102] = _settings.Reserved51.First;
            settingsSerialized[103] = _settings.Reserved51.Second;


            settingsSerialized[104] = bp52.First;
            settingsSerialized[105] = bp52.Second;


            settingsSerialized.SerializeShortLowFirst(106, (short) (_settings.UmodThr * 1000.0m));
            settingsSerialized.SerializeShortLowFirst(108, (short) (_settings.EmdecDfdt * 10.0m));
            settingsSerialized.SerializeShortLowFirst(110, _settings.TextMax);
            settingsSerialized.SerializeShortLowFirst(112, _settings.ToHl);

            var result = new byte[115];
            result[0] = OneBasedAinNumber;
            settingsSerialized.CopyTo(result, 1);
            return result;
        }

        public int ReplyLength => 1;

        public byte[] GetTestReply()
        {
            var result = new[] {OneBasedAinNumber};
            return result;
        }
    }
}