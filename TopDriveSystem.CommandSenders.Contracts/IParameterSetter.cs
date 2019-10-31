using System;

namespace TopDriveSystem.CommandSenders.Contracts
{
    public interface IParameterSetter
    {
        void SetParameterAsync(int zeroBasedParameterNumber, ushort value, Action<Exception> callback);
    }

    public sealed class ParameterSetterNothing : IParameterSetter
    {
        public void SetParameterAsync(int zeroBasedParameterNumber, ushort value, Action<Exception> callback)
        {
            throw new NotImplementedException();
        }
    }
}