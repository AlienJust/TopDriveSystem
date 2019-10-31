using System.Collections.Generic;
using NCalc;

namespace TopDriveSystem.Parameters
{
    internal sealed class ParameterInjectionConfigNcalc : IParameterInjectionConfiguration
    {
        private readonly bool _isInverBytesOrderRequired;
        private readonly string _ncalcConvertExpression;

        public ParameterInjectionConfigNcalc(int zeroBasedParameterNumber, string ncalcConvertExpression,
            bool isInverBytesOrderRequired, IList<ParameterPreselectedValue> preselectedValues)
        {
            ZeroBasedParameterNumber = zeroBasedParameterNumber;
            _ncalcConvertExpression = ncalcConvertExpression;
            _isInverBytesOrderRequired = isInverBytesOrderRequired;
            PreselectedValueList = preselectedValues;
        }

        public IList<ParameterPreselectedValue> PreselectedValueList { get; }

        public int ZeroBasedParameterNumber { get; }

        public ushort GetValue(double value)
        {
            var expr = new Expression(_ncalcConvertExpression);
            expr.Parameters.Add("value", value);

            var result = (double) expr.Evaluate();

            // TODO: use _isInverBytesOrderRequired
            return (ushort) result;
        }
    }
}