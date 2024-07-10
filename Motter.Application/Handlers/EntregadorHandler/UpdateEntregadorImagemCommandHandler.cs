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
    public class UpdateEntregadorImagemCommandHandler : IRequestHandler<UpdateEntregadorImagemCommand, Unit>
    {
        private readonly IEntregadorRepository _entregadorRepository;
        private readonly IImagemStorageService _imagemStorageService;
        private readonly ILogger<UpdateEntregadorImagemCommandHandler> _logger; // Adicione um logger

        public UpdateEntregadorImagemCommandHandler(
            IEntregadorRepository entregadorRepository,
            IImagemStorageService imagemStorageService,
            ILogger<UpdateEntregadorImagemCommandHandler> logger)
        {
            _entregadorRepository = entregadorRepository;
            _imagemStorageService = imagemStorageService;
            _logger = logger;
        }

        public async Task<Unit> Handle(UpdateEntregadorImagemCommand request, CancellationToken cancellationToken)
        {
            var entregador = await _entregadorRepository.GetByIdAsync(request.Id);
            if (entregador == null)
            {
                throw new KeyNotFoundException("Entregador não encontrado.");
            }

            if (!string.IsNullOrEmpty(entregador.ImagemCNHUrl))
            {
                // Verificar se a imagem existe no diretório
                if (File.Exists(entregador.ImagemCNHUrl))
                {
                    await _imagemStorageService.DeleteImageAsync(entregador.ImagemCNHUrl);
                    _logger.LogInformation("Imagem antiga excluída: {ImagemUrl}", entregador.ImagemCNHUrl);
                }
                else
                {
                    _logger.LogWarning("Imagem antiga não encontrada no diretório: {ImagemUrl}", entregador.ImagemCNHUrl);
                }
            }

            var imageUrl = await _imagemStorageService.SaveImageAsync(request.Imagem);
            entregador.ImagemCNHUrl = imageUrl;
            await _entregadorRepository.UpdateAsync(entregador);

            return Unit.Value;
        }
    }
}
