using AutoMapper;
using Hotels.Application.DTOs;
using Hotels.Application.Interfaces;
using Hotels.Application.Queries;
using Hotels.SharedKernel;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Hotels.Application.Handlers
{
    public class GetPropertyByIdHandler : IRequestHandler<GetPropertyByIdQuery, Result<PropertyDto>>
    {
        private readonly IPropertyRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetPropertyByIdHandler> _logger;

        public GetPropertyByIdHandler(IPropertyRepository repository, IMapper mapper, ILogger<GetPropertyByIdHandler> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Result<PropertyDto>> Handle(GetPropertyByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Fetching property with Id: {PropertyId}", request.Id);

                var property = await _repository.GetPropertyByIdAsync(request.Id);
                if (property == null)
                {
                    _logger.LogWarning("Property with Id {PropertyId} not found.", request.Id);
                    return Result<PropertyDto>.Failure($"Property with id {request.Id} not found");
                }

                var dto = _mapper.Map<PropertyDto>(property);
                var traces = await _repository.GetPropertyTracesAsync(property.IdProperty);
                dto.Traces = _mapper.Map<List<PropertyTraceDto>>(traces);

                _logger.LogInformation("Property with Id {PropertyId} fetched successfully.", request.Id);
                return Result<PropertyDto>.Success(dto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching property with Id {PropertyId}", request.Id);
                return Result<PropertyDto>.Failure("An error occurred while fetching the property.");
            }
        }
    }
}
