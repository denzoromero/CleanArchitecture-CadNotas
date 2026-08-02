using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.CadNotasCore.Disciplinas.Commands.Update
{
    public class UpdateDisciplinaValidator : AbstractValidator<UpdateDisciplina>
    {
        public UpdateDisciplinaValidator() 
        {
            RuleFor(v => v.IdempotencyKey)
               .NotEmpty()
               .WithMessage("IdempotencyKey is required.");

            RuleFor(v => v.Id)
              .NotEmpty()
              .WithMessage("Id is empty.")
              .GreaterThan(0)
              .WithMessage("Id is invalid.");

            RuleFor(v => v.Disciplina)
                    .NotEmpty()
                    .WithMessage("Disciplina is required.");
        }
    }
}
