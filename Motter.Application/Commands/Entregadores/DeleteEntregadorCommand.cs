using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Motter.Application.Commands.Entregadores
{
    public class DeleteEntregadorCommand : IRequest<Unit>
    {
        public Guid Id { get; set; }
    }
}
