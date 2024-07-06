using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Motter.Application.Interfaces
{
    public  interface IPlacaUnicaValidator
    {
        bool IsPlacaUnica(string placa);
    }
}
