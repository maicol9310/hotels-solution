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
            return await _context.Properties.Find(p => p.IdProperty == id).FirstOrDefaultAsync();
        }
    }
}
