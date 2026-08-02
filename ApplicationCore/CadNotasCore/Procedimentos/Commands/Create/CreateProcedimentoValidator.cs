using ApplicationCore.CadNotasCore.MaterialCLVMs.Commands.Create;
using ApplicationCore.Common.Validators.Creates;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.CadNotasCore.Procedimentos.Commands.Create
{
    public class CreateProcedimentoValidator : CreateCommonValidator<CreateProcedimento>
    {
        public CreateProcedimentoValidator()
        {
            RuleFor(x => x.Procedimento)
            .NotEmpty()
            .WithMessage("Procedimento is required.");
        }
    }
}
