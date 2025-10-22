namespace Hotels.Domain.Entities
{
    public class Owner
    {
        public string IdOwner { get; private set; } = string.Empty;
        public string Name { get; private set; } = string.Empty;
        public string Address { get; private set; } = string.Empty;
        public string? Photo { get; private set; }
        public DateTime? Birthday { get; private set; }
        public Owner() { }
    }
}
