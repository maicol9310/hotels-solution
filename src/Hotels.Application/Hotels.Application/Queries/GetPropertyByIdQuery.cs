using Hotels.Application.DTOs;
using MediatR;

namespace Hotels.Application.Queries
{
    public record GetPropertyByIdQuery(string Id) : IRequest<PropertyDto>;
}