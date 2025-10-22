using AutoMapper;
using Hotels.Application.Commands;
using Hotels.Application.DTOs;
using Hotels.Application.Interfaces;
using Hotels.Domain.Entities;
using Hotels.Domain.ValueObjects;
using Hotels.SharedKernel;
using MediatR;

namespace Hotels.Application.Handlers
{
    public class CreatePropertyHandler : IRequestHandler<CreatePropertyCommand, Result<PropertyDto>>
    {
        private readonly IPropertyRepository _repository;
        private readonly IMapper _mapper;

        public CreatePropertyHandler(IPropertyRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Result<PropertyDto>> Handle(CreatePropertyCommand request, CancellationToken cancellationToken)
        {
            var owner = await _repository.GetOwnerByIdAsync(request.OwnerId);
            if (owner == null)
                return Result<PropertyDto>.Failure("Owner not found");

            var address = new Address(request.Street, request.City, request.Country);

            var image = request.ImageFile != null
                ? new PropertyImage { IdPropertyImage = Guid.NewGuid().ToString(), File = request.ImageFile, Enabled = true }
                : null;

            var property = Property.Create(
                idProperty: request.IdProperty,
                name: request.Name,
                address: address,
                price: request.Price,
                codeInternal: request.CodeInternal,
                year: request.Year,
                owner: owner,
                image: image
            );

            await _repository.AddPropertyAsync(property);

            var dto = _mapper.Map<PropertyDto>(property);
            return Result<PropertyDto>.Success(dto);
        }
    }
}
