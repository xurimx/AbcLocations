using FluentValidation;
using Locations.Application.Accounts.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Locations.Application.Accounts.Validators
{
    public class RefreshTokenRequestValidator : AbstractValidator<RefreshTokenRequest>
    {
        public RefreshTokenRequestValidator()
        {
            RuleFor(x => x.Token).NotNull().NotEmpty();
            RuleFor(x => x.RefreshToken).NotNull().NotEmpty();
        }        
    }
}
