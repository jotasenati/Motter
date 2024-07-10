using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Motter.Application.Commands.Entregadores
{
    public  class UpdateEntregadorImagemCommand : IRequest<Unit>
    {
        public Guid Id { get; set; }
        public IFormFile Imagem { get; set; }
    }
}
