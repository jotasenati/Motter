using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Motter.Application.Commands.Locacoes;
using Motter.Application.DTOs;
using Motter.Application.Queries.Locacoes;

namespace Motter.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocacoesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LocacoesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/locacoes/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<LocacaoDto>> GetLocacao(Guid id)
        {
            var locacao = await _mediator.Send(new GetLocacaoByIdQuery { Id = id });
            return locacao != null ? Ok(locacao) : NotFound();
        }

        // GET: api/locacoes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LocacaoDto>>> GetAllLocacoes()
        {
            var locacoes = await _mediator.Send(new GetAllLocacoesQuery());
            return Ok(locacoes);
        }

        // POST: api/locacoes
        [HttpPost]
        public async Task<ActionResult<LocacaoDto>> CreateLocacao(CreateLocacaoCommand command)
        {
            try
            {
                var locacao = await _mediator.Send(command);
                return CreatedAtAction(nameof(GetLocacao), new { id = locacao.Id }, locacao);
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Errors);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // PUT: api/locacoes/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult<LocacaoDto>> UpdateLocacao(Guid id, UpdateLocacaoCommand command)
        {
            if (id != command.Id)
                return BadRequest("O ID da locação informado na URL não corresponde ao ID no corpo da requisição.");

            try
            {
                var locacao = await _mediator.Send(command);
                return Ok(locacao);
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Errors);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
