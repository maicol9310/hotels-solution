using Hotels.Application.DTOs;
using Hotels.SharedKernel;
using MediatR;

namespace Hotels.Application.Queries
{
    public record GetPropertiesQuery() : IRequest<Result<IEnumerable<PropertyDto>>>;
}