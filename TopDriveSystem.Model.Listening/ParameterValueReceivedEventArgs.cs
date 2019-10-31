using System;

namespace TopDriveSystem.Model.Listening
{
    public sealed class ParameterValueReceivedEventArgs : EventArgs
    {
        public ParameterValueReceivedEventArgs(string parameterId, double value)
        {
            ParameterId = parameterId;
            Value = value;
        }

        public string ParameterId { get; }

        public double Value { get; }
    }
}