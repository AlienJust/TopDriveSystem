using NCalc;

namespace TopDriveSystem.ControlApp.ViewModels.ParameterPresentation
{
    public sealed class BooleanView : IParameterView
    {
        private readonly string _expression;
        private readonly string _resultTrueFormat;
        private readonly string _resultFalseFormat;

        public string Name { get; }

        public BooleanView(string expression, string resultTrueFormat, string resultFalseFormat, string name)
        {
            _expression = expression;
            _resultTrueFormat = resultTrueFormat;
            _resultFalseFormat = resultFalseFormat;
            Name = name;
        }

        public string GetText(double value)
        {
            var expr = new Expression(_expression);
            expr.Parameters.Add("value", value);
            bool result = (bool)expr.Evaluate();
            return result ? _resultTrueFormat : _resultFalseFormat;
        }
    }
}