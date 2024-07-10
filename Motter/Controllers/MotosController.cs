using MediatR;
using Microsoft.AspNetCore.Mvc;
using Motter.Application.Commands.Motos;
using Motter.Application.DTOs;
using Motter.Application.Queries.Motos;
using Motter.Domain.Entities;

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

        [HttpGet("{placa}")]
        public async Task<ActionResult<MotoDto>> Get(string placa)
        {
            var moto = await _mediator.Send(new GetAllMotos { Placa = placa });
            if (moto == null)
            {
                return NotFound();
            }
            return Ok(moto);
        }

        [HttpPost("Insert")]
        public async Task<ActionResult<Moto>> Create([FromBody] CreateMotoCommand command)
        {
            var moto = await _mediator.Send(command);
            return CreatedAtAction(nameof(Get), new { id = moto.Id }, moto);
        }

        [HttpDelete]
        public async Task<ActionResult<bool>> DeleteMoto([FromQuery] string placa)
        {
            try
            {
                await _mediator.Send(new DeleteMotoCommand { placa = placa });
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (Exception ex) // Captura a exceção de locações associadas
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
