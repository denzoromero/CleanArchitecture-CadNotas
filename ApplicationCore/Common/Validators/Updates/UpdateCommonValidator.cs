using ApplicationCore.Interfaces;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Common.Validators.Updates
{
    public abstract class UpdateCommonValidator<T> : AbstractValidator<T> where T : IIdempotentRequest, IEntityRequest
    {
        protected UpdateCommonValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0);

            RuleFor(x => x.IdempotencyKey)
                .NotEmpty()
                .WithMessage("IdempotencyKey is required.");
        }
    }
}
