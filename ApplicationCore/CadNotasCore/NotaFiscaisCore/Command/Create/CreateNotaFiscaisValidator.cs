using ApplicationCore.Common.Validators.Creates;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.CadNotasCore.NotaFiscaisCore.Command.Create
{
    public class CreateNotaFiscaisValidator : CreateCommonValidator<CreateNotaFiscais>
    {
        public CreateNotaFiscaisValidator()
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
