using MediatR;
using Microsoft.AspNetCore.Mvc;
using Motter.Application.Commands.Motos;
using Motter.Application.Queries.Motos;

namespace Motter.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MotosController : ControllerBase
    {
        private readonly IMediator _mediator;

        public MotosController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MotoDto>> Get(Guid id)
        {
            var moto = await _mediator.Send(new GetMotoByIdQuery { Id = id });
            if (moto == null)
            {
                return NotFound();
            }
            return Ok(moto);
        }

        [HttpPost]
        public async Task<ActionResult<MotoDto>> Create(CreateMotoCommand command)
        {
            var moto = await _mediator.Send(command);
            return CreatedAtAction(nameof(Get), new { id = moto.Id }, moto);
        }

        // ... (demais actions para PUT, DELETE, etc.)
    }
}
