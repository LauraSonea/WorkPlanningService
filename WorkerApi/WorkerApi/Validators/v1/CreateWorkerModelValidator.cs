using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkerApi.Models.v1;

namespace WorkerApi.Validators.v1
{
    public class CreateWorkerModelValidator: AbstractValidator<CreateWorkerModel>
    {
        public CreateWorkerModelValidator()
        {
            RuleFor(x => x.FirstName)
               .NotNull()
               .WithMessage("The first name must be at least 2 character long");
            RuleFor(x => x.FirstName)
                .MinimumLength(2).
                WithMessage("The first name must be at least 2 character long");

            RuleFor(x => x.LastName)
                .NotNull()
                .WithMessage("The last name must be at least 2 character long");
            RuleFor(x => x.LastName)
                .MinimumLength(2)
                .WithMessage("The last name must be at least 2 character long");

            RuleFor(x => x.Age)
                .InclusiveBetween(0, 150)
                .WithMessage("The minimum age is 0 and the maximum age is 150 years");
        }
    }
}
