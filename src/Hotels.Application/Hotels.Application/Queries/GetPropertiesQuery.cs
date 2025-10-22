using Hotels.Application.DTOs;
using MediatR;

namespace Hotels.Application.Queries
{
    public record GetPropertiesQuery() : IRequest<IEnumerable<PropertyDto>>;

}