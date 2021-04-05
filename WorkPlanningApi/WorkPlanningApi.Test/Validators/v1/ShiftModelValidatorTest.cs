using FluentValidation.TestHelper;
using System;
using WorkPlanningApi.Validators.v1;
using Xunit;

namespace WorkPlanningApi.Test.Validators.v1
{
    public class ShiftModelValidatorTest
    {
        private readonly ShiftModelValidator _testee;

        public ShiftModelValidatorTest()
        {
            _testee = new ShiftModelValidator();
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("a")]
        public void WorkerFullName_WhenShorterThanTwoCharacter_ShouldHaveValidationError(string customerFullName)
        {
            _testee.ShouldHaveValidationErrorFor(x => x.WorkerFullName, customerFullName).WithErrorMessage("The worker name must be at least 2 character long");
        }

        [Fact]
        public void CustomerFullName_WhenLongerThanTwoCharacter_ShouldNotHaveValidationError()
        {
            _testee.ShouldNotHaveValidationErrorFor(x => x.WorkerFullName, "Ab");
        }


        [Fact]
        public void Startdate_InTheFuture_ShouldNotHaveValidationError()
        {
            _testee.ShouldNotHaveValidationErrorFor(x => x.StartDate, DateTime.Now.AddDays(1));
        }

        [Fact]
        public void Enddate_InTheFuture_ShouldNotHaveValidationError()
        {
            _testee.ShouldNotHaveValidationErrorFor(x => x.EndDate, DateTime.Now.AddDays(1));
        }

        [Fact]
        public void Enddate_InThePast_ShouldHaveValidationError()
        {
            _testee.ShouldHaveValidationErrorFor(x => x.EndDate, DateTime.Now.AddDays(-1)).WithErrorMessage("The endate date of a shift must in the future");
        }

        [Fact]
        public void Startdate_InThePast_ShouldHaveValidationError()
        {
            _testee.ShouldHaveValidationErrorFor(x => x.StartDate, DateTime.Now.AddDays(-1)).WithErrorMessage("The start date of a shift must in the future");
        }
    }
}
