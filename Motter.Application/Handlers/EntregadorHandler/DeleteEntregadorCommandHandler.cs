using MediatR;
using Microsoft.Extensions.Logging;
using Motter.Application.Commands.Entregadores;
using Motter.Application.Interfaces;
using Motter.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Motter.Application.Handlers.EntregadorHandler
{
    public class DeleteEntregadorCommandHandler : IRequestHandler<DeleteEntregadorCommand, Unit>
    {
        private readonly IEntregadorRepository _entregadorRepository;
        private readonly IImagemStorageService _imagemStorageService;
        private readonly ILogger<DeleteEntregadorCommandHandler> _logger;

        public DeleteEntregadorCommandHandler(
            IEntregadorRepository entregadorRepository,
            IImagemStorageService imagemStorageService,
            ILogger<DeleteEntregadorCommandHandler> logger)
        {
            _entregadorRepository = entregadorRepository;
            _imagemStorageService = imagemStorageService;
            _logger = logger;
        }

        public async Task<Unit> Handle(DeleteEntregadorCommand request, CancellationToken cancellationToken)
        {
            var entregador = await _entregadorRepository.GetByIdAsync(request.Id);
            if (entregador == null)
            {
                throw new KeyNotFoundException("Entregador não encontrado.");
            }

            if (!string.IsNullOrEmpty(entregador.ImagemCNHUrl))
            {
                await _imagemStorageService.DeleteImageAsync(entregador.ImagemCNHUrl);
                _logger.LogInformation("Imagem da CNH excluída: {ImagemUrl}", entregador.ImagemCNHUrl);
            }

            await _entregadorRepository.DeleteAsync(entregador);
            _logger.LogInformation("Entregador excluído: {EntregadorId}", request.Id);

            return Unit.Value;
        }
    }
}
