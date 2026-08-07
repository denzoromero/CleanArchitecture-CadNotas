using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.CadNotasCore.NotaFiscaisCore.Queries
{
    public class MakeMascaraValidator : AbstractValidator<MakeMascara>
    {
        public MakeMascaraValidator() 
        {
            RuleFor(x => x.IdObra)
         .GreaterThan(0)
         .WithMessage("Obra must be greater than zero.");
        }
    }
}
