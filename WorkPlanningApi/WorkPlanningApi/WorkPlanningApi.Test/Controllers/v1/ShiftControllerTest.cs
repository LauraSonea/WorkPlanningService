using AutoMapper;
using FakeItEasy;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkPlanningApi.Models.v1;
using WorkPlanningApi.Controllers.v1;
using WorkingPlanningApi.Service.v1.Command;
using WorkPlanningApi.Domain.Entities;
using System.Threading;
using WorkingPlanningApi.Service.v1.Query;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using FluentAssertions;
using System.Net;

namespace WorkPlanningApi.Test.Controllers.v1
{
    public class ShiftControllerTest
    {
        private readonly ShiftController _testee;
        private readonly ShiftModel _shiftModel;
        private readonly ShiftModel _shiftModel2;
        private readonly ShiftModel _shiftModel3;
        private readonly ShiftModel _shiftModel4;
        private readonly Guid _id = Guid.Parse("bffcf83a-0224-4a7c-a278-5aae00a02c1e");
        private readonly IMediator _mediator;

        public ShiftControllerTest()
        {
            _mediator = A.Fake<IMediator>();
            _testee = new ShiftController(A.Fake<IMapper>(), _mediator);

            _shiftModel = new ShiftModel
            {
                WorkerFullName = "Darth Vader",
                WorkerGuid = Guid.Parse("9f35b48d-cb87-4783-bfdb-21e36012930a"),
                StartDate = new DateTime(2021, 04, 10),
                EndDate = new DateTime(2021,04, 11)
            };

            _shiftModel2 = new ShiftModel
            {
                WorkerFullName = "Lauri",
                WorkerGuid = Guid.Parse("9f35b48d-cb87-4783-bfdb-21e36012930a"),
                StartDate = new DateTime(2021, 04, 8),
                EndDate = new DateTime(2021, 04, 9)
            };

            _shiftModel3 = new ShiftModel
            {
                WorkerFullName = "Lauri",
                WorkerGuid = Guid.Parse("9f35b48d-cb87-4783-bfdb-21e36012930a"),
                StartDate = new DateTime(2021, 04, 9),
                EndDate = new DateTime(2021, 04, 10)
            };

            _shiftModel4 = new ShiftModel
            {
                WorkerFullName = "Lauri",
                WorkerGuid = Guid.Parse("9f35b48d-cb87-4783-bfdb-21e36012930a"),
                StartDate = new DateTime(2021, 04, 10, 12, 0, 0),
                EndDate = new DateTime(2021, 04, 11)
            };

            var shifts = new List<Shift>
            {
                new Shift
                {
                    WorkerFullName = "Lauri",
                    WorkerGuid = Guid.Parse("9f35b48d-cb87-4783-bfdb-21e36012930a"),
                    Id = _id,
                    StartDate = new DateTime(2021, 04, 09),
                    EndDate = new DateTime(2021, 04, 10),
                },
                 new Shift
                {
                    WorkerFullName = "Lauri",
                    WorkerGuid = Guid.Parse("9f35b48d-cb87-4783-bfdb-21e36012930a"),
                    Id = Guid.Parse("8f35b48d-cb87-4783-bfdb-21e36012930a"),
                    StartDate = new DateTime(2021, 04, 13),
                    EndDate = new DateTime(2021, 04, 13),
                },
                new Shift
                {
                    WorkerFullName = "Catalin",
                    WorkerGuid = Guid.Parse("bffcf83a-0224-4a7c-a278-5aae00a02c1e"),
                    Id = Guid.Parse("9f35b48d-cb87-4783-bfdb-21e36012930a"),
                    StartDate = new DateTime(2021, 04, 08),
                    EndDate = new DateTime(2021, 04, 09),
                }
            };


            A.CallTo(() => _mediator.Send(A<CreateShiftCommand>._, A<CancellationToken>._)).Returns(shifts.First());
            A.CallTo(() => _mediator.Send(A<GetLastWorkedShiftByWorkerIdQuery>._, A<CancellationToken>._)).Returns(shifts.Last());
            A.CallTo(() => _mediator.Send(A<GetShiftByWorkerGuidQuery>._, A<CancellationToken>._)).Returns(shifts.Where(x=>x.WorkerGuid == Guid.Parse("9f35b48d-cb87-4783-bfdb-21e36012930a")).ToList());
            A.CallTo(() => _mediator.Send(A<GetShiftByIdQuery>._, A<CancellationToken>._)).Returns(shifts.Find(x => x.Id == Guid.Parse("9f35b48d-cb87-4783-bfdb-21e36012930a")));

        }

        [Theory]
        [InlineData("CreateShiftAsync: shift is null")]
        public async void Shift_WhenAnExceptionOccurs_ShouldReturnBadRequest(string exceptionMessage)
        {
            A.CallTo(() => _mediator.Send(A<CreateShiftCommand>._, default)).Throws(new ArgumentException(exceptionMessage));

            var result = await _testee.Shift(_shiftModel);

            (result.Result as StatusCodeResult)?.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);
            (result.Result as BadRequestObjectResult)?.Value.Should().Be(exceptionMessage);
        }

        [Theory]
        [InlineData("A worker can not work two shifts at the same time")]
        public async void Shift_ShouldReturnError2(string exceptionMessage)
        {
            A.CallTo(() => _mediator.Send(A<CreateShiftCommand>._, default)).Throws(new ArgumentException(exceptionMessage));

            var result = await _testee.Shift(_shiftModel2);

            (result.Result as StatusCodeResult)?.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);
            (result.Result as BadRequestObjectResult)?.Value.Should().Be(exceptionMessage);
        }


        [Theory]
        [InlineData("A worker never has two shifts in a row")]
        public async void Shift_ShouldReturnError3(string exceptionMessage)
        {
            A.CallTo(() => _mediator.Send(A<CreateShiftCommand>._, default)).Throws(new ArgumentException(exceptionMessage));

            var result = await _testee.Shift(_shiftModel3);

            (result.Result as StatusCodeResult)?.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);
            (result.Result as BadRequestObjectResult)?.Value.Should().Be(exceptionMessage);
        }


        [Theory]
        [InlineData("It is a 24 hour timetable")]
        public async void Shift_ShouldReturnError4(string exceptionMessage)
        {
            A.CallTo(() => _mediator.Send(A<CreateShiftCommand>._, default)).Throws(new ArgumentException(exceptionMessage));

            var result = await _testee.Shift(_shiftModel4);

            (result.Result as StatusCodeResult)?.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);
            (result.Result as BadRequestObjectResult)?.Value.Should().Be(exceptionMessage);
        }

        [Fact]
        public async void Shift_ShouldReturnShift()
        {
            var result = await _testee.Shift(_shiftModel);

            (result.Result as StatusCodeResult)?.StatusCode.Should().Be((int)HttpStatusCode.OK);
            result.Value.Should().BeOfType<Shift>();
            result.Value.Id.Should().Be(_id);
        }

        [Fact]
        public async void Shifts_WhenNoShiftsWereFound_ShouldReturnEmptyList()
        {
            A.CallTo(() => _mediator.Send(A<GetShiftByWorkerGuidQuery>._, A<CancellationToken>._)).Returns(new List<Shift>());

            var result = await _testee.Shifts(_id);

            (result.Result as StatusCodeResult)?.StatusCode.Should().Be((int)HttpStatusCode.OK);
            result.Value.Should().BeOfType<List<Shift>>();
            result.Value.Count.Should().Be(0);
        }


        [Fact]
        public async void Shifts_ShouldReturnListOfShifts()
        {
            var result = await _testee.Shifts(_id);

            (result.Result as StatusCodeResult)?.StatusCode.Should().Be((int)HttpStatusCode.OK);
            result.Value.Should().BeOfType<List<Shift>>();
            result.Value.Count.Should().Be(2);
        }
    }
}
