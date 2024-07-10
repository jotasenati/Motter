using MediatR;
using Motter.Application.Queries.Motos;
using Motter.Domain.Entities;
using Motter.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Motter.Application.Handlers.MotoHandler
{
    public class GetMotoByIdQueryHandler : IRequestHandler<GetAllMotos, IEnumerable<Moto>>
    {
        private readonly IMotoRepository _motoRepository;

        public GetMotoByIdQueryHandler(IMotoRepository motoRepository)
        {
            _motoRepository  = motoRepository;
        }

        public async Task<IEnumerable<Moto>> Handle(GetAllMotos request, CancellationToken cancellationToken)
        {
            return await _motoRepository.GetAllAsync(request.Placa);
        }
    }
}
