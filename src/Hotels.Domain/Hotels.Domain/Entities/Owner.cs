using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Hotels.Domain.Entities
{
    public class Owner
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty; // _id de MongoDB

        public string IdOwner { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string? Photo { get; set; }
        public DateTime? Birthday { get; set; }
        public Owner() { }
    }
}
