using FluentValidation;
using Motter.Application.Commands.Entregadores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Motter.Application.Validators.EntregadorValidator
{
    public class UpdateEntregadorImagemCNHCommandValidator : AbstractValidator<UpdateEntregadorImagemCommand>
    {
        public UpdateEntregadorImagemCNHCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("O ID do entregador é obrigatório.");
            RuleFor(x => x.Imagem).NotNull().WithMessage("A imagem da CNH é obrigatória.");
        }
    }
}
