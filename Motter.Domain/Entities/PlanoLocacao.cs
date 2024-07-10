using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Motter.Domain.Entities
{
    public class PlanoLocacao
    {
        public int Id { get; private set; }
        public int DuracaoDias { get; private set; }
        public decimal ValorDiaria { get; private set; }

    }
}
