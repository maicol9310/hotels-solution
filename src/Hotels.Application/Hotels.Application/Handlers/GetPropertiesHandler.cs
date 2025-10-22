using AutoMapper;
using Hotels.Application.DTOs;
using Hotels.Application.Interfaces;
using Hotels.Application.Queries;
using Hotels.SharedKernel;
using MediatR;

namespace Hotels.Application.Handlers
{
    public class GetPropertiesHandler : IRequestHandler<GetPropertiesQuery, Result<IEnumerable<PropertyDto>>>
    {
        private readonly IPropertyRepository _repository;
        private readonly IMapper _mapper;

    public GetPropertiesHandler(IPropertyRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<PropertyDto>>> Handle(GetPropertiesQuery request, CancellationToken cancellationToken)
        {
            var properties = await _repository.GetAllPropertiesAsync();

            var propertyDtos = new List<PropertyDto>();

            foreach (var property in properties)
            {
                // Mapeo base
                var dto = _mapper.Map<PropertyDto>(property);

                // Obtener los traces de la propiedad
                var traces = await _repository.GetPropertyTracesAsync(property.IdProperty);

                // Mapear los traces al DTO correspondiente
                dto.Traces = _mapper.Map<IEnumerable<PropertyTraceDto>>(traces);

                propertyDtos.Add(dto);
            }

            return Result<IEnumerable<PropertyDto>>.Success(propertyDtos);
        }
    }

}
