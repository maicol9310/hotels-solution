using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Hotels.Domain.Entities
{
    public class Property
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty; // _id de MongoDB

        public string IdProperty { get; set; } = string.Empty; // tu ID de negocio

        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string CodeInternal { get; set; } = string.Empty;
        public int Year { get; set; }
        public Owner? Owner { get; set; }
        public PropertyImage? Image { get; set; }
    }
}
