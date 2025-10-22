using Hotels.Application.Handlers;
using Hotels.Application.Interfaces;
using Hotels.Application.Mapping;
using Hotels.Infrastructure.Persistence;
using Hotels.Infrastructure.Repositories;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// MediatR
builder.Services.AddMediatR(typeof(GetPropertiesHandler).Assembly);

// DI Application + Infrastructure
builder.Services.AddScoped<IPropertyRepository, PropertyRepository>();
builder.Services.AddSingleton<MongoDbContext>();

// Automapper
builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddProfile<MappingProfile>();
});

var app = builder.Build();

// Configure Middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

// Seed Database
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<MongoDbContext>();
    await SeedData.InitializeAsync(dbContext);
}

app.Run();
