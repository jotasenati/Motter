using MediatR;
using Motter.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Motter.Application.Commands.Motos
{
    public class CreateMotoCommand : IRequest<MotoDto>
    {
        public Guid Id { get; set; }
        public string Identificador { get; set; }
        public int Ano { get; set; }
        public string Modelo { get; set; }
        public string Placa { get; set; }
        public DateTime DataCadastro { get; set; }
    }
}
