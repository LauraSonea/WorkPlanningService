using FluentValidation;
using System;
using WorkPlanningApi.Models.v1;

namespace WorkPlanningApi.Validators.v1
{
    public class ShiftModelValidator: AbstractValidator<ShiftModel>
    {
        public ShiftModelValidator()
        {
            RuleFor(x => x.WorkerFullName)
               .NotNull()
               .WithMessage("The worker name must be at least 2 character long");
           
            RuleFor(x => x.WorkerFullName)
                .MinimumLength(2).WithMessage("The worker name must be at least 2 character long");
            
            RuleFor(x => x.StartDate)
              .GreaterThanOrEqualTo(DateTime.Now)
              .WithMessage("The start date of a shift must in the future");

            RuleFor(x => x.EndDate)
                .GreaterThanOrEqualTo(DateTime.Now)
                .WithMessage("The endate date of a shift must in the future");

        }
    }
}
