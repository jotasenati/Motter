using MediatR;
using Microsoft.AspNetCore.Mvc;
using Motter.Application.Commands.Entregadores;
using Motter.Application.DTOs;
using Motter.Application.Queries.Entregadores;
using Motter.Application.Queries.Motos;

namespace Motter.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EntregadoresController : ControllerBase
    {
        private readonly IMediator _mediator;

        public EntregadoresController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/entregadores
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EntregadorDto>>> GetAllEntregadores([FromQuery] string? cnpj = null)
        {
            var entregadores = await _mediator.Send(new GetAllEntregadoresQuery { Cnpj = cnpj });
            return Ok(entregadores);
        }

        // POST: api/entregadores
        [HttpPost]
        public async Task<ActionResult<EntregadorDto>> CreateEntregador(CreateEntregadorCommand command)
        {
            var entregador = await _mediator.Send(command);
            return CreatedAtAction(nameof(CreateEntregador), new { id = entregador.Id }, entregador);
        }

        // PUT: api/entregadores/imagem-cnh
        [HttpPut("imagem-cnh")]
        public async Task<IActionResult> UpdateEntregadorImagemCNH(UpdateEntregadorImagemCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }

        // DELETE: api/entregadores/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEntregador(Guid id)
        {
            await _mediator.Send(new DeleteEntregadorCommand { Id = id });
            return NoContent();
        }
    }
}
