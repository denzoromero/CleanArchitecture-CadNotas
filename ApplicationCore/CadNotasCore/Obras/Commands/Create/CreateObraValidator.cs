using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.CadNotasCore.Obras.Commands.Create
{
    public class CreateObraValidator : AbstractValidator<CreateObra>
    {
        public CreateObraValidator() 
        {
            RuleFor(v => v.IdempotencyKey)
                   .NotEmpty()
                   .WithMessage("IdempotencyKey is required.");

            RuleFor(v => v.Obra)
                .NotEmpty()
                .WithMessage("Obra is required.");

            RuleFor(v => v.Cliente)
            .NotEmpty()
            .WithMessage("Cliente is required.");

        }
    }
}
