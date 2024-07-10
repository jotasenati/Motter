using MediatR;
using Motter.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Motter.Application.Queries.Motos
{
    public class GetAllMotos : IRequest<IEnumerable<Moto>>
    {
        public string? Placa { get; set; }
    }
}
