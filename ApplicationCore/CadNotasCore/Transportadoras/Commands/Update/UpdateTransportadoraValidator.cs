using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.CadNotasCore.Transportadoras.Commands.Update
{
    public class UpdateTransportadoraValidator : AbstractValidator<UpdateTransportadora>
    {
        public UpdateTransportadoraValidator()
        {
            RuleFor(v => v.IdempotencyKey)
                  .NotEmpty()
                  .WithMessage("IdempotencyKey is required.");

            RuleFor(v => v.Id)
              .NotEmpty()
              .WithMessage("Id is empty.")
              .GreaterThan(0)
              .WithMessage("Id is invalid.");

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
