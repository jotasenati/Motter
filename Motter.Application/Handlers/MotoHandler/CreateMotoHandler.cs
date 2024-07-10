using AutoMapper;
using FluentValidation;
using MediatR;
using Motter.Application.Commands.Motos;
using Motter.Application.DTOs;
using Motter.Application.Interfaces;
using Motter.Application.Validators;
using Motter.Domain.Entities;
using Motter.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Motter.Application.Handlers.MotoHandler
{
    public class CreateMotoHandler : IRequestHandler<CreateMotoCommand, MotoDto>
    {
        private readonly IMotoRepository _motoRepository;
        private readonly IValidator<CreateMotoCommand> _validator;
        private readonly IMessageProducer _messageProducer;
        private readonly IMapper _mapper;


        public CreateMotoHandler(IMotoRepository motoRepository, IValidator<CreateMotoCommand> validator, IMessageProducer messageProducer, IMapper mapper)
        {
            _motoRepository = motoRepository;
            _validator = validator;
            _messageProducer = messageProducer;
            _mapper = mapper;
        }

        public async Task<MotoDto> Handle(CreateMotoCommand request, CancellationToken cancellationToken)
        {
            var moto = new Moto();

            try
            {
                moto = _mapper.Map<Moto>(request);

                _validator.ValidateAndThrow(request);

                await _motoRepository.AddAsync(moto);
                await _messageProducer.PublishAsync("moto_exchange", "moto.cadastrada", moto);
            }
            catch (ValidationException ex)
            {
                throw new ValidationException(ex.Errors);
            }

            return _mapper.Map<MotoDto>(moto);
        }
    }
}
