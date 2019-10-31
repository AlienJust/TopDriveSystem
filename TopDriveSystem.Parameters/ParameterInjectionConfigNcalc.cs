using NCalc;
using System.Collections.Generic;

namespace TopDriveSystem.ControlApp.ViewModels.ParameterPresentation
{
    internal sealed class ParameterInjectionConfigNcalc : IParameterInjectionConfiguration
    {
        private readonly string _ncalcConvertExpression;
        private readonly bool _isInverBytesOrderRequired;

        public IList<ParameterPreselectedValue> PreselectedValueList { get; }

        public int ZeroBasedParameterNumber { get; }

        public ParameterInjectionConfigNcalc(int zeroBasedParameterNumber, string ncalcConvertExpression, bool isInverBytesOrderRequired, IList<ParameterPreselectedValue> preselectedValues)
        {
            ZeroBasedParameterNumber = zeroBasedParameterNumber;
            _ncalcConvertExpression = ncalcConvertExpression;
            _isInverBytesOrderRequired = isInverBytesOrderRequired;
            PreselectedValueList = preselectedValues;
        }

        public ushort GetValue(double value)
        {
            var expr = new Expression(_ncalcConvertExpression);
            expr.Parameters.Add("value", value);

            double result = (double)expr.Evaluate();

            // TODO: use _isInverBytesOrderRequired
            return (ushort)result;
        }
    }
}