using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Motter.Domain.Entities
{
    public class Entregador
    {
        public Guid Id { get;  set; }
        public string Nome { get; private set; }
        public string CNPJ { get; private set; }
        public DateTime DataNascimento { get; private set; }
        public string NumeroCNH { get; private set; }
        public string TipoCNH { get; private set; }
        public string ImagemCNHUrl { get;  set; }

    }
}
