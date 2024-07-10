using FluentValidation;
using Motter.Application.Commands.Locacoes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Motter.Application.Validators.LocacaoValidator
{
    public class UpdateLocacaoCommandValidator : AbstractValidator<UpdateLocacaoCommand>
    {
        public UpdateLocacaoCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("O ID da locação é obrigatório.");
            RuleFor(x => x.DataTerminoReal).NotEmpty().WithMessage("A data de término real é obrigatória.");
        }
    }
}
