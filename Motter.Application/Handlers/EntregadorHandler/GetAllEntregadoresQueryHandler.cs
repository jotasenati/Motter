using AutoMapper;
using MediatR;
using Motter.Application.DTOs;
using Motter.Application.Queries.Entregadores;
using Motter.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Motter.Application.Handlers.EntregadorHandler
{
    public class GetAllEntregadoresQueryHandler : IRequestHandler<GetAllEntregadoresQuery, IEnumerable<EntregadorDto>>
    {
        private readonly IEntregadorRepository _entregadorRepository;
        private readonly IMapper _mapper;

        public GetAllEntregadoresQueryHandler(IEntregadorRepository entregadorRepository, IMapper mapper)
        {
            _entregadorRepository = entregadorRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<EntregadorDto>> Handle(GetAllEntregadoresQuery request, CancellationToken cancellationToken)
        {
            var entregadores = await _entregadorRepository.GetAllAsync(request.Cnpj);
            return _mapper.Map<IEnumerable<EntregadorDto>>(entregadores);
        }
    }
}
