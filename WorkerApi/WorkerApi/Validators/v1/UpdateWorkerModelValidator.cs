using FluentValidation;
using WorkerApi.Models.v1;

namespace WorkerApi.Validators.v1
{
    public class UpdateWorkerModelValidator: AbstractValidator<UpdateWorkerModel>
    {
        public UpdateWorkerModelValidator()
        {
            RuleFor(x => x.FirstName)
                .NotNull()
                .MinimumLength(2).
                WithMessage("The first name must be at least 2 character long");

            RuleFor(x => x.LastName)
                .NotNull()
                .MinimumLength(2)
                .WithMessage("The last name must be at least 2 character long");

            RuleFor(x => x.Age)
                .InclusiveBetween(0, 150)
                .WithMessage("The minimum age is 0 and the maximum age is 150 years");
        }
    }
}
