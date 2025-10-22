using Hotels.Domain.Entities;
using Hotels.Domain.ValueObjects;
using Hotels.Infrastructure.Persistence;
using MongoDB.Driver;

namespace Hotels.Infrastructure.Seed
{
    public static class SeedData
    {
        public static async Task InitializeAsync(MongoDbContext context)
        {
            var existingProps = await context.Properties.CountDocumentsAsync(_ => true);
            if (existingProps > 0) return;

        // ======= OWNERS =======
        var owners = new List<Owner>
        {
            new Owner
            {
                IdOwner = "O1",
                Name = "Carlos Pérez",
                Address = "Cra 45 #23-10, Medellín",
                Photo = "https://example.com/photos/carlos.jpg"
            },
            new Owner
            {
                IdOwner = "O2",
                Name = "María Fernanda Gómez",
                Address = "Av. 9 #14-55, Cali",
                Photo = "https://example.com/photos/maria.jpg"
            },
            new Owner
            {
                IdOwner = "O3",
                Name = "Jhonatan Rodríguez",
                Address = "Cll 85 #12-40, Bogotá",
                Photo = "https://example.com/photos/jhonatan.jpg"
            }
        };

            await context.Owners.InsertManyAsync(owners);

            // ======= IMAGES =======
            var images = new List<PropertyImage>
        {
            new PropertyImage
            {
                IdPropertyImage = "IMG1",
                File = "https://example.com/images/apto_medellin.jpg",
                Enabled = true
            },
            new PropertyImage
            {
                IdPropertyImage = "IMG2",
                File = "https://example.com/images/casa_cali.jpg",
                Enabled = true
            },
            new PropertyImage
            {
                IdPropertyImage = "IMG3",
                File = "https://example.com/images/finca_bogota.jpg",
                Enabled = true
            }
        };

            await context.PropertyImages.InsertManyAsync(images);

            // ======= PROPERTIES =======
            var properties = new List<Property>
        {
            new Property
            {
                IdProperty = "P1",
                Name = "Apartamento El Poblado",
                Address = new Address("Cra 36 #12-60", "Medellín", "Colombia"),
                Price = 550_000_000m,
                CodeInternal = "MDL-001",
                Year = 2018,
                Owner = owners[0],
                Image = images[0]
            },
            new Property
            {
                IdProperty = "P2",
                Name = "Casa Campestre Pance",
                Address = new Address("Km 2 vía Pance", "Cali", "Colombia"),
                Price = 1_200_000_000m,
                CodeInternal = "CLI-002",
                Year = 2021,
                Owner = owners[1],
                Image = images[1]
            },
            new Property
            {
                IdProperty = "P3",
                Name = "Finca Subachoque",
                Address = new Address("Vereda San Miguel", "Bogotá", "Colombia"),
                Price = 950_000_000m,
                CodeInternal = "BGT-003",
                Year = 2016,
                Owner = owners[2],
                Image = images[2]
            }
        };

            await context.Properties.InsertManyAsync(properties);

            // ======= PROPERTY TRACES =======
            var traces = new List<PropertyTrace>
        {
            new PropertyTrace
            {
                IdPropertyTrace = "T1",
                IdProperty = "P1",
                DateSale = new DateTime(2021, 03, 20),
                Name = "Venta inicial Medellín",
                Value = 480_000_000m,
                Tax = 24_000_000m
            },
            new PropertyTrace
            {
                IdPropertyTrace = "T2",
                IdProperty = "P1",
                DateSale = new DateTime(2024, 07, 05),
                Name = "Reventa con remodelación",
                Value = 550_000_000m,
                Tax = 27_500_000m
            },
            new PropertyTrace
            {
                IdPropertyTrace = "T3",
                IdProperty = "P2",
                DateSale = new DateTime(2022, 11, 11),
                Name = "Venta familiar Cali",
                Value = 1_000_000_000m,
                Tax = 50_000_000m
            },
            new PropertyTrace
            {
                IdPropertyTrace = "T4",
                IdProperty = "P3",
                DateSale = new DateTime(2020, 08, 10),
                Name = "Compra inicial Finca Subachoque",
                Value = 800_000_000m,
                Tax = 40_000_000m
            },
            new PropertyTrace
            {
                IdPropertyTrace = "T5",
                IdProperty = "P3",
                DateSale = new DateTime(2023, 02, 22),
                Name = "Actualización de avalúo",
                Value = 950_000_000m,
                Tax = 47_500_000m
            }
        };

            await context.PropertyTraces.InsertManyAsync(traces);
        }
    }

}
