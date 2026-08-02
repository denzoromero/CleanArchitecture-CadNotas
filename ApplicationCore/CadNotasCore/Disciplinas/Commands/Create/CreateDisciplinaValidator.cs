using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.CadNotasCore.Disciplinas.Commands.Create
{
    public class CreateDisciplinaValidator : AbstractValidator<CreateDisciplina>
    {
        public CreateDisciplinaValidator()
        {
            RuleFor(v => v.IdempotencyKey)
             .NotEmpty()
             .WithMessage("IdempotencyKey is required.");

            RuleFor(v => v.Disciplina)
                    .NotEmpty()
                    .WithMessage("Disciplina is required.");
        }
    }
}
