namespace Hotels.Domain.ValueObjects
{
    public record PropertyId
    {
        public string Value { get; }
        public PropertyId(string value)
        {
            if (string.IsNullOrWhiteSpace(value)) throw new ArgumentException("Id required", nameof(value));
            Value = value;
        }

        public override string ToString() => Value;
    }
}
