namespace TopDriveSystem.Parameters.Views
{
    internal sealed class DefaultView : IParameterView
    {
        public string Name => "DefaultView";

        public string GetText(double value)
        {
            return value.ToString("f2");
        }
    }
}