using Hotels.Application.Interfaces;
using Hotels.Domain.Entities;
using Hotels.Infrastructure.Persistence;
using MongoDB.Driver;

namespace Hotels.Infrastructure.Repositories
{
    public class PropertyRepository : IPropertyRepository
    {
        private readonly MongoDbContext _context;

        public PropertyRepository(MongoDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Property>> GetAllPropertiesAsync()
        {
            return await _context.Properties.Find(_ => true).ToListAsync();
        }

        public async Task<Property?> GetPropertyByIdAsync(string id)
        {
            // Buscamos por IdProperty (business id) o por mongo _id según prefieras
            var byBusinessId = await _context.Properties.Find(p => p.IdProperty == id).FirstOrDefaultAsync();
            if (byBusinessId != null) return byBusinessId;

            // fallback por mongo _id
            return await _context.Properties.Find(p => p.Id == id).FirstOrDefaultAsync();
        }

        public async Task AddPropertyAsync(Property property)
        {
            await _context.Properties.InsertOneAsync(property);
        }

        public async Task<Owner?> GetOwnerByIdAsync(string id)
        {
            return await _context.Owners.Find(o => o.IdOwner == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<PropertyTrace>> GetPropertyTracesAsync(string propertyId)
        {
            return await _context.PropertyTraces.Find(t => t.IdProperty == propertyId).ToListAsync();
        }

        public async Task AddPropertyTraceAsync(PropertyTrace trace)
        {
            await _context.PropertyTraces.InsertOneAsync(trace);
        }
    }
}
