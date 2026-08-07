using ApplicationCore.Common.Validators.Updates;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.CadNotasCore.NotaFiscaisCore.Command.Update
{
    public class UpdateNotaFiscaisValidator : UpdateCommonValidator<UpdateNotaFiscais>
    {
        public UpdateNotaFiscaisValidator() 
        {
            RuleFor(x => x.IdObra)
            .GreaterThan(0)
            .WithMessage("Obra must be greater than zero.");

            RuleFor(x => x.Data)
            .NotEqual(DateTime.MinValue)
            .WithMessage("Data is required.");
        }
    }
}
