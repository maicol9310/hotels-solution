namespace Hotels.Domain.Entities
{
    public class Property
    {
        public string IdProperty { get; private set; } = string.Empty;
        public string Name { get; private set; } = string.Empty;
        public string Address { get; private set; } = string.Empty;
        public decimal Price { get; private set; }
        public string CodeInternal { get; private set; } = string.Empty;
        public int Year { get; private set; }
        public string IdOwner { get; private set; } = string.Empty;

        public Owner? Owner { get; private set; }
        public PropertyImage? Image { get; private set; }

        public Property() { }
    }
}
