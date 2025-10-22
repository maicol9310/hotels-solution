using Hotels.Application.DTOs;
using Hotels.Application.Interfaces;
using Hotels.Application.Queries;
using MediatR;

namespace Hotels.Application.Handlers
{
    public class GetPropertiesHandler : IRequestHandler<GetPropertiesQuery, IEnumerable<PropertyDto>>
    {
        private readonly IPropertyRepository _repository;

        public GetPropertiesHandler(IPropertyRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<PropertyDto>> Handle(GetPropertiesQuery request, CancellationToken cancellationToken)
        {
            var properties = await _repository.GetAllPropertiesAsync();

            return properties.Select(p => new PropertyDto
            {
                IdProperty = p.IdProperty,
                Name = p.Name,
                Address = p.Address,
                Price = p.Price,
                CodeInternal = p.CodeInternal,
                Year = p.Year,
                Owner = p.Owner == null ? new OwnerDto() : new OwnerDto
                {
                    IdOwner = p.Owner.IdOwner,
                    Name = p.Owner.Name,
                    Address = p.Owner.Address,
                    Photo = p.Owner.Photo
                },
                ImageFile = p.Image?.File
            });
        }
    }
}