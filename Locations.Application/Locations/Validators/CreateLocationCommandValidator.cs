using FluentValidation;
using Locations.Application.Locations.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Locations.Application.Locations.Validators
{
    public class CreateLocationCommandValidator : AbstractValidator<CreateLocationCommand>
    {
        public CreateLocationCommandValidator()
        {
            RuleFor(x => x.Address).NotEmpty();
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Latitude).GreaterThanOrEqualTo(-90).LessThanOrEqualTo(90);
            RuleFor(x => x.Longitude).GreaterThanOrEqualTo(-180).LessThanOrEqualTo(180);
        }
    }
}
