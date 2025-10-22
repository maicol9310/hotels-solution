namespace Hotels.Domain.Entities
{
    public class PropertyImage
    {
        public string IdPropertyImage { get; private set; } = string.Empty;
        public string IdProperty { get; private set; } = string.Empty;
        public string File { get; private set; } = string.Empty;
        public bool Enabled { get; private set; }

        public PropertyImage() { }
    }
}
