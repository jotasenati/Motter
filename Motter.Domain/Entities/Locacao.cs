using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Motter.Domain.Entities
{
    public class Locacao
    {
        public Guid Id { get; private set; }
        public Moto Moto { get; private set; }
        public Entregador Entregador { get; private set; }
        public DateTime DataInicio { get; private set; }
        public DateTime DataTerminoPrevista { get; private set; }
        public DateTime? DataTerminoReal { get; private set; }
        public decimal ValorTotal { get; private set; }
        public int PlanoId { get; private set; } 

    }
}
