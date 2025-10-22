using Hotels.Application.DTOs;
using Hotels.SharedKernel;
using MediatR;

namespace Hotels.Application.Commands
{
    public record CreatePropertyCommand(
        string IdProperty,
        string Name,
        string Street,
        string City,
        string Country,
        decimal Price,
        string CodeInternal,
        int Year,
        string OwnerId,
        string? ImageFile
    ) : IRequest<Result<PropertyDto>>;
}
