using FluentValidation;
using Motter.Application.Commands.Motos;
using Motter.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Motter.Application.Validators
{
    public class MotoValidator : AbstractValidator<CreateMotoCommand>
    {
        private readonly IPlacaUnicaValidator _placaUnicaValidator;

        public MotoValidator(IPlacaUnicaValidator placaUnicaValidator)
        {
            _placaUnicaValidator = placaUnicaValidator;

            RuleFor(x => x.Identificador).NotEmpty();
            RuleFor(x => x.Placa)
                .NotEmpty()
                .Must(placa => _placaUnicaValidator.IsPlacaUnica(placa))
                .WithMessage("Placa já cadastrada.");
        }
    }
}
