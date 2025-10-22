using Hotels.Domain.ValueObjects;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Hotels.Domain.Entities
{
    public class Property
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty; 

        public string IdProperty { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;
        public Address? Address { get; set; } = new Address("", "", "");
        public decimal Price { get; set; }
        public string CodeInternal { get; set; } = string.Empty;
        public int Year { get; set; }
        public Owner? Owner { get; set; }

        public PropertyImage? Image { get; set; }

        public Property() { }

        public static Property Create(string idProperty, string name, Address address, decimal price, string codeInternal, int year, Owner owner, PropertyImage? image = null)
        {
            if (owner == null) throw new ArgumentNullException(nameof(owner));
            if (string.IsNullOrWhiteSpace(idProperty)) throw new ArgumentException("IdProperty required", nameof(idProperty));
            return new Property
            {
                IdProperty = idProperty,
                Name = name,
                Address = address,
                Price = price,
                CodeInternal = codeInternal,
                Year = year,
                Owner = owner,
                Image = image
            };
        }
    }
}
