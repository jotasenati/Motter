using MediatR;
using Motter.Application.Queries.Motos;
using Motter.Domain.Entities;
using Motter.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Motter.Application.Handlers
{
    public class GetMotoByIdQueryHandler : IRequestHandler<GetMotoByIdQuery, Moto>
    {
        private readonly IMotoRepository _motoRepository;

        public GetMotoByIdQueryHandler(IMotoRepository motoRepository)
        {
            _motoRepository = motoRepository;
        }

        public async Task<Moto> Handle(GetMotoByIdQuery request, CancellationToken cancellationToken)
        {
            return await _motoRepository.GetByIdAsync(request.Id);
        }
    }
}
