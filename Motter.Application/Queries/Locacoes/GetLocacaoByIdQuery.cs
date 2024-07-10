using MediatR;
using Motter.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Motter.Application.Queries.Locacoes
{
    public class GetLocacaoByIdQuery : IRequest<LocacaoDto>
    {
        public Guid Id { get; set; }
    }
}
