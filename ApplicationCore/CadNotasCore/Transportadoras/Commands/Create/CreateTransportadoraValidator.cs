using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.CadNotasCore.Transportadoras.Commands.Create
{
    public class CreateTransportadoraValidator : AbstractValidator<CreateTransportadora>
    {
        public CreateTransportadoraValidator() 
        {
            RuleFor(v => v.IdempotencyKey)
                    .NotEmpty()
                    .WithMessage("IdempotencyKey is required.");

            RuleFor(v => v.Nome)
                    .NotEmpty()
                    .WithMessage("Nome is required.");

            RuleFor(v => v.IE)
                    .NotEmpty()
                    .WithMessage("IE is required.");


            RuleFor(v => v.CNPJ)
                    .NotEmpty()
                    .WithMessage("CNPJ is required.");

        }
    }
}
