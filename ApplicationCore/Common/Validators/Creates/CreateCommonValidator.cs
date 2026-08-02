using ApplicationCore.Interfaces;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Common.Validators.Creates
{
    public abstract class CreateCommonValidator<T> : AbstractValidator<T> where T : IIdempotentRequest
    {
        protected CreateCommonValidator()
        {
            RuleFor(x => x.IdempotencyKey)
                .NotEmpty()
                .WithMessage("IdempotencyKey is required.");
        }
    }
}
