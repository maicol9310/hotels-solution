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
using MongoDB.Driver;    
using Serilog;
using Serilog.Events;
using System.Reflection;

BsonSerializer.RegisterSerializer(typeof(Address), new AddressSerializer());

var builder = WebApplication.CreateBuilder(args);

// Load settings
var mongoSettings = builder.Configuration.GetSection("MongoDbSettings").Get<MongoDbSettings>();
if (mongoSettings == null)
    throw new InvalidOperationException("MongoDbSettings missing from configuration.");

var mongoUrlBuilder = new MongoUrlBuilder(mongoSettings.ConnectionString)
{
    DatabaseName = mongoSettings.DatabaseName // asegura que el nombre de BD quede establecido
};

var mongoConnectionWithDb = mongoUrlBuilder.ToString(); // URI canonificada y correcta

// Configure Serilog (console + mongo sink)
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .WriteTo.MongoDB(
        databaseUrl: mongoConnectionWithDb,
        collectionName: "ApplicationLogs",
        restrictedToMinimumLevel: LogEventLevel.Information)
    .CreateLogger();

builder.Host.UseSerilog();

builder.Services.Configure<MongoDbSettings>(builder.Configuration.GetSection("MongoDbSettings"));
builder.Services.AddSingleton<MongoDbContext>();
builder.Services.AddScoped<IPropertyRepository, PropertyRepository>();

// MediatR / AutoMapper / Controllers
builder.Services.AddMediatR(Assembly.GetExecutingAssembly(), typeof(GetPropertiesHandler).Assembly);
builder.Services.AddAutoMapper(cfg => { }, typeof(MappingProfile).Assembly);
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

app.UseSerilogRequestLogging();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.MapControllers();

Log.Information("Starting API and Serilog configured to write to MongoDB: {Db}", mongoSettings.DatabaseName);

app.Run();

Log.CloseAndFlush();
