namespace TopDriveSystem.ControlApp.ViewModels.ParameterPresentation
{
    public interface IParameterView
    {
        string GetText(double value);
        string Name { get; }
    }

    internal enum BytesOrder
    {
        LittleEndian,
        BigEndian
    }

    public struct ParameterPreselectedValue
    {
        public string Text { get; }
        public double Value { get; }

        public ParameterPreselectedValue(string text, double value)
        {
            Text = text;
            Value = value;
        }

        public bool Equals(ParameterPreselectedValue other)
        {
            return Text == other.Text && Value.Equals(other.Value);
        }

        public override bool Equals(object obj)
        {
            return obj is ParameterPreselectedValue other && Equals(other);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((Text != null ? Text.GetHashCode() : 0) * 397) ^ Value.GetHashCode();
            }
        }

        public static bool operator ==(ParameterPreselectedValue left, ParameterPreselectedValue right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(ParameterPreselectedValue left, ParameterPreselectedValue right)
        {
            return !(left == right);
        }
    }
}