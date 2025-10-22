namespace Hotels.Domain.Entities
{
    public class PropertyTrace
    {
        public string IdPropertyTrace { get; private set; } = string.Empty;
        public DateTime DateSale { get; private set; }
        public string Name { get; private set; } = string.Empty;
        public decimal Value { get; private set; }
        public decimal Tax { get; private set; }
        public string IdProperty { get; private set; } = string.Empty;

        public PropertyTrace() { }
    }
}
