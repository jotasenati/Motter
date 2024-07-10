using AutoMapper;
using FluentValidation;
using MediatR;
using Motter.Application.Commands.Locacoes;
using Motter.Application.DTOs;
using Motter.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Motter.Application.Handlers.LocacaoHandler
{
    public class UpdateLocacaoCommandHandler : IRequestHandler<UpdateLocacaoCommand, LocacaoDto>
    {
        private readonly ILocacaoRepository _locacaoRepository;
        private readonly IValidator<UpdateLocacaoCommand> _validator;
        private readonly IMapper _mapper;

        public UpdateLocacaoCommandHandler(
            ILocacaoRepository locacaoRepository,
            IValidator<UpdateLocacaoCommand> validator,
            IMapper mapper)
        {
            _locacaoRepository = locacaoRepository;
            _validator = validator;
            _mapper = mapper;
        }

        public async Task<LocacaoDto> Handle(UpdateLocacaoCommand request, CancellationToken cancellationToken)
        {
            _validator.ValidateAndThrow(request);

            var locacao = await _locacaoRepository.GetByIdAsync(request.Id);

            if (locacao == null)
                throw new KeyNotFoundException("Locação não encontrada.");

            locacao.FinalizarLocacao(request.DataTerminoReal ?? DateTime.Now);

            await _locacaoRepository.UpdateAsync(locacao);

            return _mapper.Map<LocacaoDto>(locacao);
        }
    }
}
