using System.Collections.Generic;

namespace TopDriveSystem.Parameters.Views
{
    internal sealed class MultiView : IParameterView
    {
        private readonly string _expression;
        private readonly IList<IParameterView> _subViews;

        public MultiView(string expression, IList<IParameterView> subViews, string name)
        {
            _expression = expression;
            _subViews = subViews;
            Name = name;
        }

        public string Name { get; }

        public string GetText(double value)
        {
            var text = _expression;
            foreach (var v in _subViews) text = text.Replace(v.Name, v.GetText(value));
            return text;
        }
    }
}