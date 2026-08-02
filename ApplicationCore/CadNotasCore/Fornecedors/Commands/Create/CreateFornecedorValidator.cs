using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.CadNotasCore.Fornecedors.Commands.Create
{
    public class CreateFornecedorValidator : AbstractValidator<CreateFornecedor>
    {
        public CreateFornecedorValidator()
        {
            RuleFor(v => v.IdempotencyKey)
                          .NotEmpty()
                    .WithMessage("IdempotencyKey is required.");

            RuleFor(v => v.Fornecedor)
                    .MaximumLength(100)
                    .WithMessage("Fornecedor cannot exceed 100 characters.")
                    .NotEmpty()
                    .WithMessage("Fornecedor is required.");

            RuleFor(v => v.Fantasia)
                    .MaximumLength(50)
                    .WithMessage("Fantasia cannot exceed 50 characters.")
                    .NotEmpty()
                    .WithMessage("Fantasia is required.");

            RuleFor(v => v.IE)
                    .MaximumLength(15)
                    .WithMessage("IE cannot exceed 15 characters.")
                    .NotEmpty()
                    .WithMessage("IE is required.");

            RuleFor(v => v.CNPJ)
                     .MaximumLength(20)
                     .WithMessage("CNPJ cannot exceed 20 characters.")
                     .NotEmpty()
                     .WithMessage("CNPJ is required.");

        }
    }
}
