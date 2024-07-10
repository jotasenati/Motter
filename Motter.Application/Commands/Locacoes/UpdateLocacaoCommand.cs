using MediatR;
using Motter.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Motter.Application.Commands.Locacoes
{
    public class UpdateLocacaoCommand : IRequest<LocacaoDto>
    {
        public Guid Id { get; set; }
        public DateTime? DataTerminoReal { get; set; }
    }
}
