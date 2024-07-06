using AutoMapper;
using MediatR;
using Motter.Application.Commands.Motos;
using Motter.Application.Interfaces;
using Motter.Application.Validators;
using Motter.Domain.Entities;
using Motter.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Motter.Application.Handlers
{
    public class CreateMotoHandler : IRequestHandler<CreateMotoCommand, Moto>
    {
        private readonly IMotoRepository _motoRepository;
        private readonly MotoValidator _motoValidator;
        private readonly IMessageProducer _messageProducer;
        private readonly IMapper _mapper;


        public CreateMotoHandler(IMotoRepository motoRepository, MotoValidator motoValidator, IMessageProducer messageProducer, IMapper mapper)
        {
            _motoRepository = motoRepository;
            _motoValidator = motoValidator;
            _messageProducer = messageProducer;
            _mapper = mapper;
        }

        public async Task<Moto> Handle(CreateMotoCommand request, CancellationToken cancellationToken)
        {
            _motoValidator.Validate(request); // Validação separada

            var moto = _mapper.Map<Moto>(request); 

            await _motoRepository.AddAsync(moto);
            await _messageProducer.PublishAsync("moto_exchange", "moto.cadastrada", moto);

            return moto;
        }
    }
}
