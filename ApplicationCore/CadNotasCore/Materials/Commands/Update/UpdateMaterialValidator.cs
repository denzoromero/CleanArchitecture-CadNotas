using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.CadNotasCore.Materials.Commands.Update
{
    public class UpdateMaterialValidator : AbstractValidator<UpdateMaterial>
    {
        public UpdateMaterialValidator() 
        {
            RuleFor(v => v.IdempotencyKey)
                 .NotEmpty()
                 .WithMessage("IdempotencyKey is required.");

            RuleFor(v => v.Id)
              .NotEmpty()
              .WithMessage("Id is empty.")
              .GreaterThan(0)
              .WithMessage("Id is invalid.");

            RuleFor(v => v.Material)
                    .NotEmpty()
                    .WithMessage("Material is required.");
        }
    }
}
