using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.CadNotasCore.Materials.Commands.Create
{
    public class CreateMaterialValidator : AbstractValidator<CreateMaterial>
    {
        public CreateMaterialValidator()
        {
            RuleFor(v => v.IdempotencyKey)
              .NotEmpty()
              .WithMessage("IdempotencyKey is required.");

            RuleFor(v => v.Material)
                    .NotEmpty()
                    .WithMessage("Material is required.");
        }
    }
}
