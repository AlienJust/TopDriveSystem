﻿using System;
using TopDriveSystem.Commands.Contracts;

namespace TopDriveSystem.CommandSenders.Contracts
{
    public interface IRrModbusCommandSender
    {
        void SendCommandAsync(byte address, IRrModbusCommandWithReply command, TimeSpan timeout, int maxAttemptsCount,
            Action<Exception, byte[]> onComplete);

        //void SendCommandAsyncNoLog(byte address, IRrModbusCommandWithReply command, TimeSpan timeout, Action<Exception, byte[]> onComplete);
    }
}