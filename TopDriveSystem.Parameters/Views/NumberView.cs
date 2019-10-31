using NCalc;

namespace TopDriveSystem.ControlApp.ViewModels.ParameterPresentation
{
    internal sealed class NumberView : IParameterView
    {
        private readonly string _expression;
        private readonly string _resultStringFormat;
        public string Name { get; }

        public NumberView(string expression, string resultStringFormat, string name)
        {
            _expression = expression;
            _resultStringFormat = resultStringFormat;
            Name = name;
        }

        public string GetText(double value)
        {
            var expr = new Expression(_expression);
            expr.Parameters.Add("value", value);

            double result = (double)expr.Evaluate();
            return result.ToString(_resultStringFormat);
        }
    }
}