using NCalc;

namespace TopDriveSystem.Parameters.Views
{
    internal sealed class NumberView : IParameterView
    {
        private readonly string _expression;
        private readonly string _resultStringFormat;

        public NumberView(string expression, string resultStringFormat, string name)
        {
            _expression = expression;
            _resultStringFormat = resultStringFormat;
            Name = name;
        }

        public string Name { get; }

        public string GetText(double value)
        {
            var expr = new Expression(_expression);
            expr.Parameters.Add("value", value);

            var result = (double) expr.Evaluate();
            return result.ToString(_resultStringFormat);
        }
    }
}