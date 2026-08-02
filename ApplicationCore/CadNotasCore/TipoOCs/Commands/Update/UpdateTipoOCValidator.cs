using ApplicationCore.CadNotasCore.Disciplinas.Commands.Update;
using ApplicationCore.Common.Validators.Updates;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.CadNotasCore.TipoOCs.Commands.Update
{
    public class UpdateTipoOCValidator : UpdateCommonValidator<UpdateTipoOC>
    {
        public UpdateTipoOCValidator() 
        {
            RuleFor(x => x.Nome)
           .NotEmpty()
           .WithMessage("Nome is required.");
        }
    }
}
