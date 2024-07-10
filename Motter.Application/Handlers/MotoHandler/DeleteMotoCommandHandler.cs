using MediatR;
using Motter.Application.Commands.Motos;
using Motter.Application.Interfaces;
using Motter.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Motter.Application.Handlers.MotoHandler
{
    public class DeleteMotoCommandHandler : IRequestHandler<DeleteMotoCommand, Unit>
    {
        private readonly IMotoRepository _motoRepository;
        private readonly ILocacaoRepository _locacaoRepository;

        public DeleteMotoCommandHandler(IMotoRepository motoRepository, ILocacaoRepository locacaoRepository)
        {
            _motoRepository = motoRepository;
            _locacaoRepository = locacaoRepository;
        }

        public async Task<Unit> Handle(DeleteMotoCommand request, CancellationToken cancellationToken)
        {
            var moto = await _motoRepository.GetAllAsync(request.placa);
            if (!moto.Any())
                throw new KeyNotFoundException("Moto não encontrada.");

            if (!await _locacaoRepository.ExistsLocacaoAtivaPorMoto(request.placa))
                throw new Exception("Não é possível excluir uma moto com locações associadas.");

            await _motoRepository.DeleteAsync(moto.FirstOrDefault());
            return Unit.Value;
        }
    }
}
