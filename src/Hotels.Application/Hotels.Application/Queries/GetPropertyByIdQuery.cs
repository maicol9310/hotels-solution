using Hotels.Application.DTOs;
using Hotels.SharedKernel;
using MediatR;

namespace Hotels.Application.Queries
{
    public record GetPropertyByIdQuery(string Id) : IRequest<Result<PropertyDto>>;
}