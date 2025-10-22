using AutoMapper;
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
        private readonly IMapper _mapper;

        public GetPropertyByIdHandler(IPropertyRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Result<PropertyDto>> Handle(GetPropertyByIdQuery request, CancellationToken cancellationToken)
        {
            var property = await _repository.GetPropertyByIdAsync(request.Id);

            if (property == null)
                return Result<PropertyDto>.Failure($"Property with id {request.Id} not found");

            var dto = _mapper.Map<PropertyDto>(property);

            var traces = await _repository.GetPropertyTracesAsync(property.IdProperty);
            dto.Traces = _mapper.Map<List<PropertyTraceDto>>(traces);

            return Result<PropertyDto>.Success(dto);
        }
    }
}
