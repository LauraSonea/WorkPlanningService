using FakeItEasy;
using FluentAssertions;
using WorkingPlanningApi.Service.v1.Command;
using WorkPlanningApi.Data.Repository.v1;
using WorkPlanningApi.Domain.Entities;
using Xunit;

namespace WorkPlanningApi.Service.Test.v1.Command
{
    public class CreateShiftCommandHandlerTests
    {
        private readonly IShiftRepository _shiftRepository;
        private readonly CreateShiftCommandHandler _testee;

        public CreateShiftCommandHandlerTests()
        {
            _shiftRepository = A.Fake<IShiftRepository>();
            _testee = new CreateShiftCommandHandler(_shiftRepository);
        }

        [Fact]
        public async void Handle_ShouldReturnCreatedShift()
        {
            A.CallTo(() => _shiftRepository.AddAsync(A<Shift>._)).Returns(new Shift { WorkerFullName = "Haha Ha" });

            var result = await _testee.Handle(new CreateShiftCommand(), default);

            result.Should().BeOfType<Shift>();
            result.WorkerFullName.Should().Be("Haha Ha");
        }

        [Fact]
        public async void Handle_ShouldCallRepositoryAddAsync()
        {
            await _testee.Handle(new CreateShiftCommand(), default);

            A.CallTo(() => _shiftRepository.AddAsync(A<Shift>._)).MustHaveHappenedOnceExactly();
        }
    }
}
