﻿using System;
using TopDriveSystem.Commands.Contracts;

namespace TopDriveSystem.Commands.SetTime
{
    public class SetTimeCommand : IRrModbusCommandWithReply, IRrModbusCommandResultGetter<bool>,
        IRrModbusCommandWithTestReply
    {
        private readonly DateTime _timeToSet;

        public SetTimeCommand(DateTime timeToSet)
        {
            _timeToSet = timeToSet;
        }

        public bool GetResult(byte[] reply)
        {
            if (reply.Length != 0) throw new Exception("Неверная длина ответа");
            return true;
        }

        public byte CommandCode => 0x92;

        public string Name =>
            "Установка времени в БС-Ethernet в значение " + _timeToSet.ToString("yyyy.MM.dd-HH:mm:ss");

        public byte[] Serialize()
        {
            // first byte is low
            return new[]
            {
                (byte) _timeToSet.Second,
                (byte) _timeToSet.Minute,
                (byte) _timeToSet.Hour,
                (byte) _timeToSet.Day,
                (byte) _timeToSet.Month,
                (byte) (_timeToSet.Year - 2000)
            };
        }

        public int ReplyLength => 0;

        public byte[] GetTestReply()
        {
            return new byte[0];
        }
    }
}