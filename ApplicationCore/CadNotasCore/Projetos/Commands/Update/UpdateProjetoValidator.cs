using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.CadNotasCore.Projetos.Commands.Update
{
    public class UpdateProjetoValidator : AbstractValidator<UpdateProjeto>
    {
        public UpdateProjetoValidator() 
        {
            RuleFor(v => v.IdempotencyKey)
                 .NotEmpty()
                 .WithMessage("IdempotencyKey is required.");

            RuleFor(v => v.Id)
              .NotEmpty()
              .WithMessage("Id is empty.")
              .GreaterThan(0)
              .WithMessage("Id is invalid.");

            RuleFor(v => v.Projeto)
                    .NotEmpty()
                    .WithMessage("Projeto is required.");
        }
    }
}
