using FluentValidation;
using Locations.Application.Accounts.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Locations.Application.Accounts.Validators
{
    public class AuthenticateRequestValidator : AbstractValidator<AuthenticateRequest>
    {
        public AuthenticateRequestValidator()
        {
            RuleFor(x => x.Username).NotNull().NotEmpty().MinimumLength(5);
            RuleFor(x => x.Password).NotNull().NotEmpty().MinimumLength(6);
        }
    }
}
