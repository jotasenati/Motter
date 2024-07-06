using MediatR;
using Motter.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Motter.Application.Queries.Motos
{
    public class GetMotoByIdQuery : IRequest<Moto>
    {
        public Guid Id { get; set; }
    }
}
