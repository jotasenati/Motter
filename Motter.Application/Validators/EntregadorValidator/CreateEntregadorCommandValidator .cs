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
    public class CreateEntregadorCommandValidator : AbstractValidator<CreateEntregadorCommand>
    {
        private readonly IEntregadorRepository _entregadorRepository;

        public CreateEntregadorCommandValidator(IEntregadorRepository entregadorRepository)
        {
            _entregadorRepository = entregadorRepository;

            RuleFor(x => x.Nome)
                .NotEmpty().WithMessage("O nome é obrigatório.")
                .MaximumLength(100).WithMessage("O nome não pode exceder 100 caracteres.");

            RuleFor(x => x.CNPJ)
                .NotEmpty().WithMessage("O CNPJ é obrigatório.")
                .Must(BeValidCNPJ).WithMessage("O CNPJ informado é inválido.")
                .Must(BeUniqueCNPJ).WithMessage("O CNPJ já está cadastrado.");

            RuleFor(x => x.DataNascimento)
                .NotEmpty().WithMessage("A data de nascimento é obrigatória.")
                .LessThan(DateTime.Now.AddYears(-18)).WithMessage("O entregador deve ter pelo menos 18 anos.");

            RuleFor(x => x.NumeroCNH)
                .NotEmpty().WithMessage("O número da CNH é obrigatório.")
                .Must(BeUniqueNumeroCNH).WithMessage("O número da CNH já está cadastrado.");

            RuleFor(x => x.TipoCNH)
                .NotEmpty().WithMessage("O tipo da CNH é obrigatório.")
                .Must(BeValidTipoCNH).WithMessage("O tipo da CNH deve ser A, B ou AB.");
        }

        private bool BeValidCNPJ(string cnpj)
        {
            cnpj = new string(cnpj.Where(char.IsDigit).ToArray());

            if (cnpj.Length != 14)
                return false;
            else
                return true;
        }

        private bool BeUniqueCNPJ(string cnpj)
        {
            return !_entregadorRepository.ExistsByCNPJ(cnpj);
        }

        private bool BeUniqueNumeroCNH(string numeroCNH)
        {
            return !_entregadorRepository.ExistsByNumeroCNH(numeroCNH);
        }

        private bool BeValidTipoCNH(string tipoCNH)
        {
            return tipoCNH == "A" || tipoCNH == "B" || tipoCNH == "AB";
        }
    }
}
