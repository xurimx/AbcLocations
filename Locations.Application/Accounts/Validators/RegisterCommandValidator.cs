using FluentValidation;
using Locations.Application.Accounts.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Locations.Application.Accounts.Validators
{
    public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
    {
        public RegisterCommandValidator()
        {
            RuleFor(x => x.Email).EmailAddress();
            RuleFor(x => x.Password).MinimumLength(6);
            RuleFor(x => x.Username).MinimumLength(6);
        }
    }
}
