using MediatR;
using Microsoft.AspNetCore.Http;
using Motter.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Motter.Application.Commands.Entregadores
{
    public class CreateEntregadorCommand : IRequest<EntregadorDto>
    {
        public string Nome { get; set; }
        public string CNPJ { get; set; }
        public DateTime DataNascimento { get; set; }
        public string NumeroCNH { get; set; }
        public string TipoCNH { get; set; }
        public IFormFile ImagemCNH { get; set; }
    }
}
