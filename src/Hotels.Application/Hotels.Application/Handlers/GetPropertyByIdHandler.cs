using Hotels.Application.DTOs;
using Hotels.Application.Interfaces;
using Hotels.Application.Queries;
using Hotels.SharedKernel;
using MediatR;

namespace Hotels.Application.Handlers
{
    public class GetPropertyByIdHandler : IRequestHandler<GetPropertyByIdQuery, Result<PropertyDto>>
    {
        private readonly IPropertyRepository _repository;

        public GetPropertyByIdHandler(IPropertyRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<PropertyDto>> Handle(GetPropertyByIdQuery request, CancellationToken cancellationToken)
        {
            var property = await _repository.GetPropertyByIdAsync(request.Id);

            if (property == null)
                return Result<PropertyDto>.Failure($"Property with id {request.Id} not found");

            var dto = new PropertyDto
            {
                IdProperty = property.IdProperty,
                Name = property.Name,
                Address = property.Address,
                Price = property.Price,
                CodeInternal = property.CodeInternal,
                Year = property.Year,
                Owner = property.Owner == null ? new Hotels.Application.DTOs.OwnerDto() : new Hotels.Application.DTOs.OwnerDto
                {
                    IdOwner = property.Owner.IdOwner,
                    Name = property.Owner.Name,
                    Address = property.Owner.Address,
                    Photo = property.Owner.Photo
                },
                ImageFile = property.Image?.File
            };

            return Result<PropertyDto>.Success(dto);
        }
    }
}
