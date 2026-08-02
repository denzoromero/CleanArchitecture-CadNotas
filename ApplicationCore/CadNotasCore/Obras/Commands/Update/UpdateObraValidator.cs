using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.CadNotasCore.Obras.Commands.Update
{
    public class UpdateObraValidator : AbstractValidator<UpdateObra>
    {
        public UpdateObraValidator() 
        {
            RuleFor(v => v.IdempotencyKey)
                 .NotEmpty()
                 .WithMessage("IdempotencyKey is required.");

            RuleFor(v => v.Id)
              .NotEmpty()
              .WithMessage("Id is empty.")
              .GreaterThan(0)
              .WithMessage("Id is invalid.");

            RuleFor(v => v.Obra)
            .NotEmpty()
            .WithMessage("Obra is required.");

            RuleFor(v => v.Cliente)
            .NotEmpty()
            .WithMessage("Cliente is required.");


        }
    }
}
