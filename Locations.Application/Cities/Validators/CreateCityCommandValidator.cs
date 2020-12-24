using FluentValidation;
using Locations.Application.Cities.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Locations.Application.Cities.Validators
{
    public class CreateCityCommandValidator : AbstractValidator<CreateCityCommand>
    {
        public CreateCityCommandValidator()
        {
            RuleFor(x => x.Name).NotNull().NotEmpty();
        }
    }
}
