using Hotels.Application.DTOs;
using Hotels.Application.Queries;
using Hotels.SharedKernel;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Hotels.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PropertiesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PropertiesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            Result<IEnumerable<PropertyDto>> result = await _mediator.Send(new GetPropertiesQuery());
            if (!result.IsSuccess)
                return BadRequest(result.Error);

            return Ok(result.Value);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            Result<PropertyDto> result = await _mediator.Send(new GetPropertyByIdQuery(id));
            if (!result.IsSuccess)
                return NotFound(result.Error);

            return Ok(result.Value);
        }
    }
}
