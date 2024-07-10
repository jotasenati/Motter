using AutoMapper;
using FluentValidation;
using MediatR;
using Motter.Application.Commands.Locacoes;
using Motter.Application.DTOs;
using Motter.Application.Interfaces;
using Motter.Domain.Entities;
using Motter.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Motter.Application.Handlers.LocacaoHandler
{
    public class CreateLocacaoCommandHandler : IRequestHandler<CreateLocacaoCommand, LocacaoDto>
    {
        private readonly ILocacaoRepository _locacaoRepository;
        private readonly IMotoRepository _motoRepository;
        private readonly IEntregadorRepository _entregadorRepository;
        private readonly IValidator<CreateLocacaoCommand> _validator;
        private readonly IMapper _mapper;

        public CreateLocacaoCommandHandler(
            ILocacaoRepository locacaoRepository,
            IMotoRepository motoRepository,
            IEntregadorRepository entregadorRepository,
            IValidator<CreateLocacaoCommand> validator,
            IMapper mapper)
        {
            _locacaoRepository = locacaoRepository;
            _motoRepository = motoRepository;
            _entregadorRepository = entregadorRepository;
            _validator = validator;
            _mapper = mapper;
        }

        public async Task<LocacaoDto> Handle(CreateLocacaoCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var moto = await _motoRepository.GetAllAsync(request.placa);
            var entregador = await _entregadorRepository.GetByIdAsync(request.EntregadorId);
            var planoLocacao = await _locacaoRepository.GetPlanoLocacaoByIdAsync(request.PlanoLocacaoId);

            var dataInicio = DateTime.UtcNow.AddDays(1);
            var dataTerminoPrevista = dataInicio.AddDays(planoLocacao.DuracaoDias);

            var valorTotal = planoLocacao.ValorDiaria * planoLocacao.DuracaoDias;

            var locacao = Locacao.Create(moto.FirstOrDefault(), entregador, dataInicio, dataTerminoPrevista, valorTotal, planoLocacao);

            await _locacaoRepository.AddAsync(locacao);

            return _mapper.Map<LocacaoDto>(locacao);
        }
    }
}
