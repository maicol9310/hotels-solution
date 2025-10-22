using AutoMapper;
using Hotels.Application.DTOs;
using Hotels.Application.Interfaces;
using Hotels.Application.Queries;
using Hotels.SharedKernel;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Hotels.Application.Handlers
{
    public class GetPropertiesHandler : IRequestHandler<GetPropertiesQuery, Result<IEnumerable<PropertyDto>>>
    {
        private readonly IPropertyRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetPropertiesHandler> _logger;

        public GetPropertiesHandler(IPropertyRepository repository, IMapper mapper, ILogger<GetPropertiesHandler> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Result<IEnumerable<PropertyDto>>> Handle(GetPropertiesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Fetching all properties...");

                var properties = await _repository.GetAllPropertiesAsync();
                var propertyDtos = new List<PropertyDto>();

                foreach (var property in properties)
                {
                    var dto = _mapper.Map<PropertyDto>(property);
                    var traces = await _repository.GetPropertyTracesAsync(property.IdProperty);

                    dto.Traces = _mapper.Map<IEnumerable<PropertyTraceDto>>(traces);
                    propertyDtos.Add(dto);
                }

                _logger.LogInformation("Fetched {Count} properties successfully.", propertyDtos.Count);
                return Result<IEnumerable<PropertyDto>>.Success(propertyDtos);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching properties.");
                return Result<IEnumerable<PropertyDto>>.Failure("Error fetching properties");
            }
        }
    }
}
