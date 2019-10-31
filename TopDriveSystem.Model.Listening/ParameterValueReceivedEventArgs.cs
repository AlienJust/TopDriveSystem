using System;

namespace TopDriveSystem.Model.Listening
{
    public sealed class ParameterValueReceivedEventArgs : EventArgs
    {
        public string ParameterId { get; }

        public double Value { get; }

        public ParameterValueReceivedEventArgs(string parameterId, double value) : base()
        {
            ParameterId = parameterId;
            Value = value;
        }
    }
}