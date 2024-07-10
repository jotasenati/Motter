using AutoMapper;
using FluentValidation;
using MediatR;
using Motter.Application.Commands.Entregadores;
using Motter.Application.DTOs;
using Motter.Application.Interfaces;
using Motter.Domain.Entities;
using Motter.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Motter.Application.Handlers.EntregadorHandler
{
    public class CreateEntregadorCommandHandler : IRequestHandler<CreateEntregadorCommand, EntregadorDto>
    {
        private readonly IEntregadorRepository _entregadorRepository;
        private readonly IImagemStorageService _imagemStorageService;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateEntregadorCommand> _validator;

        public CreateEntregadorCommandHandler(
            IEntregadorRepository entregadorRepository,
            IMapper mapper,
            IImagemStorageService imagemStorageService,
            IValidator<CreateEntregadorCommand> validator)
        {
            _entregadorRepository = entregadorRepository;
            _mapper = mapper;
            _validator = validator;
            _imagemStorageService = imagemStorageService;
        }

        public async Task<EntregadorDto> Handle(CreateEntregadorCommand request, CancellationToken cancellationToken)
        {
            var entregador = new Entregador();
            _validator.ValidateAndThrow(request);
            request.DataNascimento = DateTime.SpecifyKind(request.DataNascimento, DateTimeKind.Utc);

            var save = await _imagemStorageService.SaveImageAsync(request.ImagemCNH);

            entregador = _mapper.Map<Entregador>(request);

            entregador.ImagemCNHUrl = save;
            entregador.Id = Guid.NewGuid();

            await _entregadorRepository.AddAsync(entregador);

            return _mapper.Map<EntregadorDto>(entregador);
        }
    }
}
