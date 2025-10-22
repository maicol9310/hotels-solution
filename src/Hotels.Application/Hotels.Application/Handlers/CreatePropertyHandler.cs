using AutoMapper;
using Hotels.Application.Commands;
using Hotels.Application.DTOs;
using Hotels.Application.Interfaces;
using Hotels.Domain.Entities;
using Hotels.Domain.ValueObjects;
using Hotels.SharedKernel;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Hotels.Application.Handlers
{
    public class CreatePropertyHandler : IRequestHandler<CreatePropertyCommand, Result<PropertyDto>>
    {
        private readonly IPropertyRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<CreatePropertyHandler> _logger;

        public CreatePropertyHandler(IPropertyRepository repository, IMapper mapper, ILogger<CreatePropertyHandler> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Result<PropertyDto>> Handle(CreatePropertyCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Processing property creation request for {Name}", request.Name);

            var owner = await _repository.GetOwnerByIdAsync(request.OwnerId);
            if (owner == null)
            {
                _logger.LogWarning("Owner not found: {OwnerId}", request.OwnerId);
                return Result<PropertyDto>.Failure("Owner not found");
            }

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

            _logger.LogInformation("Property {PropertyId} successfully created for owner {OwnerId}", property.IdProperty, owner.IdOwner);

            var dto = _mapper.Map<PropertyDto>(property);
            return Result<PropertyDto>.Success(dto);
        }
    }
}
