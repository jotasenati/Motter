using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Motter.Application.Commands.Motos
{
    public class DeleteMotoCommand : IRequest<Unit>
    {
        public string placa { get; set; }
    }
}
