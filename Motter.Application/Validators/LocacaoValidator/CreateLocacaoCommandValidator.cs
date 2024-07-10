using FluentValidation;
using Motter.Application.Commands.Locacoes;
using Motter.Application.Interfaces;
using Motter.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Motter.Application.Validators.LocacaoValidator
{
    public class CreateLocacaoCommandValidator : AbstractValidator<CreateLocacaoCommand>
    {
        private readonly IMotoRepository _motoRepository;
        private readonly IEntregadorRepository _entregadorRepository;
        private readonly ILocacaoRepository _locacaoRepository;

        public CreateLocacaoCommandValidator(
            IMotoRepository motoRepository,
            IEntregadorRepository entregadorRepository,
            ILocacaoRepository locacaoRepository)
        {
            _motoRepository = motoRepository;
            _entregadorRepository = entregadorRepository;
            _locacaoRepository = locacaoRepository;

            RuleFor(x => x.placa).NotEmpty().MustAsync(MotoDisponivel).WithMessage("A moto não está disponível.");
            RuleFor(x => x.EntregadorId).NotEmpty().MustAsync(EntregadorHabilitado).WithMessage("Entregador não habilitado.");
            RuleFor(x => x.PlanoLocacaoId).NotEmpty().MustAsync(PlanoLocacaoExiste).WithMessage("Plano de locação inválido.");
        }

        private async Task<bool> MotoDisponivel(string placa, CancellationToken cancellationToken)
        {
            var moto = await _motoRepository.GetAllAsync(placa);
            return moto != null && await _locacaoRepository.ExistsLocacaoAtivaPorMoto(placa);
        }

        private async Task<bool> EntregadorHabilitado(Guid entregadorId, CancellationToken cancellationToken)
        {
            var entregador = _entregadorRepository.GetByIdAsync(entregadorId).Result;
            return entregador != null && entregador.TipoCNH.Contains("A");
        }

        private async Task<bool> PlanoLocacaoExiste(int planoLocacaoId, CancellationToken cancellationToken)
        {
            return await _locacaoRepository.GetPlanoLocacaoByIdAsync(planoLocacaoId) != null;
        }
    }
}
