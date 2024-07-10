using MediatR;
using Motter.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Motter.Application.Queries.Entregadores
{
    public class GetAllEntregadoresQuery : IRequest<IEnumerable<EntregadorDto>>
    {
        public string? Cnpj { get; set; } 
    }
}
