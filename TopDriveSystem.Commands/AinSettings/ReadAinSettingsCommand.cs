using System;
using System.Linq;
using AlienJust.Support.Collections;
using AlienJust.Support.Numeric.Bits;
using TopDriveSystem.Commands.Contracts;

namespace TopDriveSystem.Commands.AinSettings
{
    public class ReadAinSettingsCommand : IRrModbusCommandWithReply, IRrModbusCommandResultGetter<IAinSettings>,
        IRrModbusCommandWithTestReply
    {
        private readonly byte _zeroBasedAinNumber;

        public ReadAinSettingsCommand(byte zeroBasedAinNumber)
        {
            _zeroBasedAinNumber = zeroBasedAinNumber;
        }

        private byte OneBasedAinNumber => (byte) (_zeroBasedAinNumber + 1);

        public IAinSettings GetResult(byte[] reply)
        {
            if (reply[0] != OneBasedAinNumber)
                throw new Exception("неверный номер АИН в ответе, ожидался " + OneBasedAinNumber);

            // TODO: check if reply[0] is equal oneBasedAinNumber
            var replyWithoutAinNumber = reply.Skip(1).ToList();


            var bp52 = new BytesPair(replyWithoutAinNumber[104], replyWithoutAinNumber[105]);
            //Console.WriteLine("<<READ>> NPRM = " + bp52.First.ToString("X2") + bp52.Second.ToString("X2"));
            var np = bp52.First & 0x1F;
            var nimpFloorCode = (bp52.First & 0xE0) >> 5;
            var fanMode = AinTelemetryFanWorkmodeExtensions.FromIoBits(bp52.Second & 0x03);
            var directCurrentMagnetization = bp52.Second.GetBit(3);

            return new AinSettingsSimple(
                new BytesPair(replyWithoutAinNumber[0], replyWithoutAinNumber[1]),
                BytesPairToDecimalQ8Converter.ConvertBytesPairToDecimalQ8(new BytesPair(replyWithoutAinNumber[2],
                    replyWithoutAinNumber[3])),
                // kiW
                (replyWithoutAinNumber[4] + (replyWithoutAinNumber[5] << 8) + (replyWithoutAinNumber[6] << 16) +
                 (replyWithoutAinNumber[7] << 24)) / 16777216.0m,
                new BytesPair(replyWithoutAinNumber[8], replyWithoutAinNumber[9]).LowFirstSignedValue / 1000.0m,
                new BytesPair(replyWithoutAinNumber[10], replyWithoutAinNumber[11]).LowFirstSignedValue,
                new BytesPair(replyWithoutAinNumber[12], replyWithoutAinNumber[13]).LowFirstSignedValue,
                new BytesPair(replyWithoutAinNumber[14], replyWithoutAinNumber[15]).LowFirstSignedValue,

                // Fnom:
                new BytesPair(replyWithoutAinNumber[16], replyWithoutAinNumber[17]).LowFirstSignedValue / 10.0m,
                // Fmax:
                new BytesPair(replyWithoutAinNumber[18], replyWithoutAinNumber[19]).LowFirstSignedValue / 10.0m,
                // DflLim:
                new BytesPair(replyWithoutAinNumber[20], replyWithoutAinNumber[21]).LowFirstSignedValue / 1000.0m,
                new BytesPair(replyWithoutAinNumber[22], replyWithoutAinNumber[23]).LowFirstSignedValue / 1000.0m,
                new BytesPair(replyWithoutAinNumber[24], replyWithoutAinNumber[25]).LowFirstSignedValue,
                new BytesPair(replyWithoutAinNumber[26], replyWithoutAinNumber[27]).LowFirstSignedValue / 1000.0m,
                new BytesPair(replyWithoutAinNumber[28], replyWithoutAinNumber[29]).LowFirstUnsignedValue,
                new BytesPair(replyWithoutAinNumber[30], replyWithoutAinNumber[31]).LowFirstUnsignedValue,
                new BytesPair(replyWithoutAinNumber[32], replyWithoutAinNumber[33]).LowFirstSignedValue,
                new BytesPair(replyWithoutAinNumber[34], replyWithoutAinNumber[35]).LowFirstSignedValue,
                new BytesPair(replyWithoutAinNumber[36], replyWithoutAinNumber[37]).LowFirstSignedValue,
                new BytesPair(replyWithoutAinNumber[38], replyWithoutAinNumber[39]).LowFirstSignedValue,

                // TauR:
                new BytesPair(replyWithoutAinNumber[40], replyWithoutAinNumber[41]).LowFirstSignedValue / 10000.0m,
                // Lm:
                new BytesPair(replyWithoutAinNumber[42], replyWithoutAinNumber[43]).LowFirstSignedValue / 100000.0m,
                // Lsl:
                new BytesPair(replyWithoutAinNumber[44], replyWithoutAinNumber[45]).LowFirstSignedValue / 1000000.0m,
                // Lrl:
                new BytesPair(replyWithoutAinNumber[46], replyWithoutAinNumber[47]).LowFirstSignedValue / 1000000.0m,

                // reserved 24:
                new BytesPair(replyWithoutAinNumber[48], replyWithoutAinNumber[49]),
                BytesPairToDecimalQ8Converter.ConvertBytesPairToDecimalQ8(new BytesPair(replyWithoutAinNumber[50],
                    replyWithoutAinNumber[51])),
                // kiFi:
                (replyWithoutAinNumber[52] + (replyWithoutAinNumber[53] << 8) + (replyWithoutAinNumber[54] << 16) +
                 (replyWithoutAinNumber[55] << 24)) / 16777216.0m,

                // reserved 28:
                new BytesPair(replyWithoutAinNumber[56], replyWithoutAinNumber[57]),

                // kpId:
                BytesPairToDecimalQ8Converter.ConvertBytesPairToDecimalQ8(new BytesPair(replyWithoutAinNumber[58],
                    replyWithoutAinNumber[59])),
                // kiId:
                (replyWithoutAinNumber[60] + (replyWithoutAinNumber[61] << 8) + (replyWithoutAinNumber[62] << 16) +
                 (replyWithoutAinNumber[63] << 24)) / 16777216.0m,

                // reserverd 32:
                new BytesPair(replyWithoutAinNumber[64], replyWithoutAinNumber[65]),

                // kpIq:
                BytesPairToDecimalQ8Converter.ConvertBytesPairToDecimalQ8(new BytesPair(replyWithoutAinNumber[66],
                    replyWithoutAinNumber[67])),
                // kiIq:
                (replyWithoutAinNumber[68] + (replyWithoutAinNumber[69] << 8) + (replyWithoutAinNumber[70] << 16) +
                 (replyWithoutAinNumber[71] << 24)) / 16777216.0m,
                new BytesPair(replyWithoutAinNumber[72], replyWithoutAinNumber[73]).LowFirstSignedValue * 0.1m,
                new BytesPair(replyWithoutAinNumber[74], replyWithoutAinNumber[75]).LowFirstSignedValue * 0.1m,
                // Unom:
                new BytesPair(replyWithoutAinNumber[76], replyWithoutAinNumber[77]).LowFirstSignedValue /
                (decimal) Math.Sqrt(2.0),
                // TauFlLim:
                new BytesPair(replyWithoutAinNumber[78], replyWithoutAinNumber[79]).LowFirstSignedValue / 10000.0m,
                // Rs:
                new BytesPair(replyWithoutAinNumber[80], replyWithoutAinNumber[81]).LowFirstSignedValue / 10000.0m,
                // fmin:
                new BytesPair(replyWithoutAinNumber[82], replyWithoutAinNumber[83]).LowFirstSignedValue / 10.0m,
                new BytesPair(replyWithoutAinNumber[84], replyWithoutAinNumber[85]).LowFirstSignedValue / 10000.0m,
                new BytesPair(replyWithoutAinNumber[86], replyWithoutAinNumber[87]).LowFirstSignedValue / 10000.0m,
                new BytesPair(replyWithoutAinNumber[88], replyWithoutAinNumber[89]).LowFirstSignedValue / 10000.0m,
                new BytesPair(replyWithoutAinNumber[90], replyWithoutAinNumber[91]).LowFirstSignedValue / 10000.0m,
                new BytesPair(replyWithoutAinNumber[92], replyWithoutAinNumber[93]).LowFirstSignedValue,
                new BytesPair(replyWithoutAinNumber[94], replyWithoutAinNumber[95]).LowFirstSignedValue,
                new BytesPair(replyWithoutAinNumber[96], replyWithoutAinNumber[97]),
                new BytesPair(replyWithoutAinNumber[98], replyWithoutAinNumber[99]),

                // reserverd 50:
                new BytesPair(replyWithoutAinNumber[100], replyWithoutAinNumber[101]),

                // reserverd 51:
                new BytesPair(replyWithoutAinNumber[102], replyWithoutAinNumber[103]),

                // Param52 (np and others):
                np,
                nimpFloorCode,
                fanMode,
                directCurrentMagnetization,
                new BytesPair(replyWithoutAinNumber[106], replyWithoutAinNumber[107]).LowFirstSignedValue / 1000.0m,
                new BytesPair(replyWithoutAinNumber[108], replyWithoutAinNumber[109]).LowFirstSignedValue / 10.0m,
                new BytesPair(replyWithoutAinNumber[110], replyWithoutAinNumber[111]).LowFirstSignedValue,
                new BytesPair(replyWithoutAinNumber[112], replyWithoutAinNumber[113]).LowFirstSignedValue,

                // Status byte:
                (replyWithoutAinNumber[114] & 0x01) == 0x01,
                (replyWithoutAinNumber[114] & 0x02) == 0x02,
                (replyWithoutAinNumber[114] & 0x04) == 0x04
            );
        }

        public byte CommandCode => 0x8F;

        public string Name => "Чтение настроек АИН #" + (_zeroBasedAinNumber + 1);

        public byte[] Serialize()
        {
            return new[] {OneBasedAinNumber};
        }

        public int ReplyLength => 1 + 114 + 1; // ain number + settings + ain link fault flags

        public byte[] GetTestReply()
        {
            var rnd = new Random();
            var result = new byte[ReplyLength];
            rnd.NextBytes(result);
            result[0] = OneBasedAinNumber;
            return result;
        }
    }
}