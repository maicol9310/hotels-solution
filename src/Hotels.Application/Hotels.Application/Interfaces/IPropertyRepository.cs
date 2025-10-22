using Hotels.Domain.Entities;

namespace Hotels.Application.Interfaces
{
    public interface IPropertyRepository
    {
        Task<IEnumerable<Property>> GetAllPropertiesAsync();
        Task<Property?> GetPropertyByIdAsync(string id);

        Task AddPropertyAsync(Property property);
        Task<Owner?> GetOwnerByIdAsync(string id);

        // Traces
        Task<IEnumerable<PropertyTrace>> GetPropertyTracesAsync(string propertyId);
        Task AddPropertyTraceAsync(PropertyTrace trace);
    }
}