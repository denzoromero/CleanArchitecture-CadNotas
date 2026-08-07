using ApplicationCore.Interfaces;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Common.Validators.Queries
{
    public abstract class CommonSearchQuery<T> : AbstractValidator<T> where T : ISearchObra
    {
        protected CommonSearchQuery() 
        {
            RuleFor(x => x.IdObra)
            .GreaterThan(0)
            .WithMessage("Obra must be greater than zero.");
        }
    }
}
