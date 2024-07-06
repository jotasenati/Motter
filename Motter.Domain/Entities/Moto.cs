using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Motter.Domain.Entities
{
    public class Moto
    {
        public Guid Id { get; private set; }
        public string Identificador { get; private set; }
        public int Ano { get; private set; }
        public string Modelo { get; private set; }
        public string Placa { get; private set; }
        public DateTime DataCadastro { get; private set; }

    }
}
