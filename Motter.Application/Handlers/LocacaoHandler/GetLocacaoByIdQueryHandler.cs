using AutoMapper;
using MediatR;
using Motter.Application.DTOs;
using Motter.Application.Interfaces;
using Motter.Application.Queries.Locacoes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Motter.Application.Handlers.LocacaoHandler
{
    public class GetLocacaoByIdQueryHandler : IRequestHandler<GetLocacaoByIdQuery, LocacaoDto>
    {
        private readonly ILocacaoRepository _locacaoRepository;
        private readonly IMapper _mapper;

        public GetLocacaoByIdQueryHandler(ILocacaoRepository locacaoRepository, IMapper mapper)
        {
            _locacaoRepository = locacaoRepository;
            _mapper = mapper;
        }

        public async Task<LocacaoDto> Handle(GetLocacaoByIdQuery request, CancellationToken cancellationToken)
        {
            var locacao = await _locacaoRepository.GetByIdAsync(request.Id);
            if (locacao == null)
                return null; 

            return _mapper.Map<LocacaoDto>(locacao);
        }
    }
}
