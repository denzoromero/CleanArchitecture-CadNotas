using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.CadNotasCore.Projetos.Commands.Create
{
    public class CreateProjetoValidator : AbstractValidator<CreateProjeto>
    {
        public CreateProjetoValidator() 
        {
            RuleFor(v => v.IdempotencyKey)
                   .NotEmpty()
                   .WithMessage("IdempotencyKey is required.");

            RuleFor(v => v.Projeto)
                    .NotEmpty()
                    .WithMessage("Projeto is required.");
        }
    }
}
