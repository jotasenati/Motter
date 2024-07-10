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
    public class GetAllLocacoesQueryHandler : IRequestHandler<GetAllLocacoesQuery, IEnumerable<LocacaoDto>>
    {
        private readonly ILocacaoRepository _locacaoRepository;
        private readonly IMapper _mapper;

        public GetAllLocacoesQueryHandler(ILocacaoRepository locacaoRepository, IMapper mapper)
        {
            _locacaoRepository = locacaoRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<LocacaoDto>> Handle(GetAllLocacoesQuery request, CancellationToken cancellationToken)
        {
            var locacoes = await _locacaoRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<LocacaoDto>>(locacoes);
        }
    }
}
