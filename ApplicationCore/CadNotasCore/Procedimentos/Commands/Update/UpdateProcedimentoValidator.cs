using ApplicationCore.Common.Validators.Updates;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.CadNotasCore.Procedimentos.Commands.Update
{
    public class UpdateProcedimentoValidator : UpdateCommonValidator<UpdateProcedimento>
    {
        public UpdateProcedimentoValidator()
        {
            RuleFor(x => x.Procedimento)
           .NotEmpty()
           .WithMessage("Procedimento is required.");
        }
    }
}
