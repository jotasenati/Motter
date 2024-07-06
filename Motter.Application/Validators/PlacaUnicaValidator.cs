using Motter.Application.Interfaces;
using Motter.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Motter.Application.Validators
{
    public class PlacaUnicaValidator : IPlacaUnicaValidator
    {
        private readonly IMotoRepository _motoRepository;

        public PlacaUnicaValidator(IMotoRepository motoRepository)
        {
            _motoRepository = motoRepository;
        }

        public bool IsPlacaUnica(string placa)
        {
            return !_motoRepository.ExistsByPlaca(placa);
        }
    }

}
