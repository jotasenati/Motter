using MediatR;
using Motter.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Motter.Application.Commands.Locacoes
{
    public class CreateLocacaoCommand : IRequest<LocacaoDto>
    {
        public string placa { get; set; }
        public Guid EntregadorId { get; set; }
        public int PlanoLocacaoId { get; set; }
        public DateTime DataTerminoPrevista { get; set; }
    }
}
