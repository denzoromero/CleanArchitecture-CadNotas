using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.UserBS.Queries
{
    public class LoginUserValidator : AbstractValidator<LoginUser>
    {
        public LoginUserValidator()
        {
            RuleFor(v => v.Username)
                 .NotEmpty()
                 .WithMessage("Username is required.");

            RuleFor(v => v.Password)
                .NotEmpty()
                .WithMessage("Password is required.");
        }
    }
}
