using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Motter.Application.DTOs
{
    public class LocacaoDto
    {
        public Guid Id { get; set; }
        public Guid MotoId { get; set; }
        public Guid EntregadorId { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataTerminoPrevista { get; set; }
        public DateTime? DataTerminoReal { get; set; }
        public decimal ValorTotal { get; set; }
        public int PlanoLocacaoId { get; set; }
    }
}
