using Hotels.Domain.Entities;
using MongoDB.Driver;

namespace Hotels.Infrastructure.Persistence
{
    public static class SeedData
    {
        public static async Task InitializeAsync(MongoDbContext context)
        {
            var existing = await context.Properties.CountDocumentsAsync(_ => true);
            if (existing > 0) return;

            var owner = new Owner
            {
                IdOwner = "O1",
                Name = "John Doe",
                Address = "123 Main St",
                Photo = "https://example.com/photo1.jpg"
            };

            await context.Owners.InsertOneAsync(owner);

            var image = new PropertyImage
            {
                IdPropertyImage = "IMG1",
                IdProperty = "P1",
                File = "https://example.com/property1.jpg",
                Enabled = true
            };

            await context.PropertyImages.InsertOneAsync(image);

            var property = new Property
            {
                IdProperty = "P1",
                Name = "Luxury Apartment",
                Address = "789 Pine St",
                Price = 250000m,
                CodeInternal = "A123",
                Year = 2020,
                Owner = owner,
                Image = image
            };

            await context.Properties.InsertOneAsync(property);
        }
    }
}
