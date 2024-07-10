using FluentValidation;
using Motter.Application.Commands.Entregadores;
using Motter.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Motter.Application.Validators.EntregadorValidator
{
    public class UpdateEntregadorCommandValidator : AbstractValidator<UpdateEntregadorCommand>
    {
        public UpdateEntregadorCommandValidator()
        {

            RuleFor(x => x.Id).NotEmpty().WithMessage("O ID do entregador é obrigatório.");
            RuleFor(x => x.Nome).NotEmpty().WithMessage("O nome é obrigatório.");
            RuleFor(x => x.CNPJ).NotEmpty().WithMessage("O CNPJ é obrigatório.");
            RuleFor(x => x.DataNascimento).NotEmpty().WithMessage("A data de nascimento é obrigatória.");
            RuleFor(x => x.NumeroCNH).NotEmpty().WithMessage("O número da CNH é obrigatório.");
            RuleFor(x => x.TipoCNH).NotEmpty().WithMessage("O tipo da CNH é obrigatório.");
        }
    }
}
