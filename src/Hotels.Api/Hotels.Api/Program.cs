using Hotels.Application.Handlers;
using Hotels.Application.Interfaces;
using Hotels.Application.Mapping;
using Hotels.Domain.ValueObjects;
using Hotels.Infrastructure.Persistence;
using Hotels.Infrastructure.Repositories;
using Hotels.Infrastructure.Seed;
using Hotels.Infrastructure.Serializers;
using MediatR;
using MongoDB.Bson.Serialization;
using System.Reflection;

BsonSerializer.RegisterSerializer(typeof(Address), new AddressSerializer());

var builder = WebApplication.CreateBuilder(args);

// Configuration: asegúrate de tener sección MongoDbSettings en appsettings.json
builder.Services.Configure<Hotels.Infrastructure.Persistence.MongoDbSettings>(builder.Configuration.GetSection("MongoDbSettings"));

// DI: MongoDbContext
builder.Services.AddSingleton<MongoDbContext>();

// Repositories
builder.Services.AddScoped<IPropertyRepository, PropertyRepository>();

// MediatR
builder.Services.AddMediatR(Assembly.GetExecutingAssembly(), typeof(GetPropertiesHandler).Assembly);

// AutoMapper
builder.Services.AddAutoMapper(cfg => { }, typeof(MappingProfile).Assembly);

// Controllers
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Seed
using (var scope = app.Services.CreateScope())
{
    var ctx = scope.ServiceProvider.GetRequiredService<MongoDbContext>();
    await SeedData.InitializeAsync(ctx);
}

// Pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.MapControllers();
app.Run();
