﻿using System;
using TopDriveSystem.Commands.Contracts;

namespace TopDriveSystem.Commands.AinCommand
{
    public class FirstAinCommand : IRrModbusCommandWithReply, IRrModbusCommandResultGetter<bool>,
        IRrModbusCommandWithTestReply
    {
        private readonly short _fset;
        private readonly short _mmax;
        private readonly short _mmin;
        private readonly ushort _modeset;
        private readonly short _mset;
        private readonly short _set3;
        private readonly byte _zeroBasedAinNumber;

        public FirstAinCommand(byte zeroBasedAinNumber, ushort modeset, short fset, short mset, short set3, short mmin,
            short mmax)
        {
            _zeroBasedAinNumber = zeroBasedAinNumber;
            _modeset = modeset;
            _fset = fset;
            _mset = mset;
            _set3 = set3;
            _mmin = mmin;
            _mmax = mmax;
        }

        public bool GetResult(byte[] reply)
        {
            if (reply.Length != 1) throw new Exception("Неверная длина ответа от АИН");

            if (reply[0] == _zeroBasedAinNumber + 1) return true;
            throw new Exception("Неверный номер АИН в ответе, ожидался №" + (_zeroBasedAinNumber + 1));
        }

        public byte CommandCode => 0x86;

        public string Name => "Команда для АИН #" + (_zeroBasedAinNumber + 1);

        public byte[] Serialize()
        {
            // first byte is low
            return new[]
            {
                (byte) (_zeroBasedAinNumber + 1), (byte) (_modeset & 0x00FF), (byte) ((_modeset & 0xFF00) >> 8),
                (byte) (_fset & 0x00FF), (byte) ((_fset & 0xFF00) >> 8), (byte) (_mset & 0x00FF),
                (byte) ((_mset & 0xFF00) >> 8), (byte) (_set3 & 0x00FF), (byte) ((_set3 & 0xFF00) >> 8),
                (byte) (_mmin & 0x00FF), (byte) ((_mmin & 0xFF00) >> 8), (byte) (_mmax & 0x00FF),
                (byte) ((_mmax & 0xFF00) >> 8)
            };
        }

        public int ReplyLength => 1;

        public byte[] GetTestReply()
        {
            var rnd = new Random();
            var result = new byte[1];
            result[0] = rnd.NextDouble() < 0.5 ? (byte) (_zeroBasedAinNumber + 1) : (byte) 255;
            return result;
        }
    }
}