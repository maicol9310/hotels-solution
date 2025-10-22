using Hotels.Domain.ValueObjects;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace Hotels.Infrastructure.Serializers
{
    public class AddressSerializer : SerializerBase<Address>
    {
        public override Address Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
        {
            var bsonType = context.Reader.GetCurrentBsonType();

            if (bsonType == BsonType.String)
            {
                var addressString = context.Reader.ReadString();
                // Si en la BD solo hay la calle, devolvemos Address mínima
                return new Address(addressString, string.Empty, string.Empty);
            }

            var document = BsonSerializer.Deserialize<BsonDocument>(context.Reader);
            var street = document.GetValue("Street", "").AsString;
            var city = document.GetValue("City", "").AsString;
            var country = document.GetValue("Country", "").AsString;

            return new Address(street, city, country);
        }

        public override void Serialize(BsonSerializationContext context, BsonSerializationArgs args, Address value)
        {
            context.Writer.WriteStartDocument();
            context.Writer.WriteName("Street");
            context.Writer.WriteString(value.Street);
            context.Writer.WriteName("City");
            context.Writer.WriteString(value.City);
            context.Writer.WriteName("Country");
            context.Writer.WriteString(value.Country);
            context.Writer.WriteEndDocument();
        }
    }
}
