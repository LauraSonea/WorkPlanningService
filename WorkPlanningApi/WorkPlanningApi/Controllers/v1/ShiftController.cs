using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkingPlanningApi.Service.v1.Command;
using WorkingPlanningApi.Service.v1.Query;
using WorkPlanningApi.Domain.Entities;
using WorkPlanningApi.Models.v1;

namespace WorkPlanningApi.Controllers.v1
{
    [Produces("application/json")]
    [Route("v1/[controller]")]
    [ApiController]
    public class ShiftController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public ShiftController(IMapper mapper, IMediator mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }
        /// <summary>
        ///     Action to create a new shift in the database.
        /// </summary>
        /// <param name="orderModel">Model to create a new shift</param>
        /// <returns>Returns the created shift</returns>
        /// <response code="200">Returned if the shift was created</response>
        /// <response code="400">Returned if the model couldn't be parsed or saved</response>
        /// <response code="422">Returned when the validation failed</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [HttpPost]
        public async Task<ActionResult<Shift>> Shift(ShiftModel shiftModel)
        {
            try
            {
                var lastShiftWorked = await _mediator.Send(new GetLastWorkedShiftByWorkerIdQuery
                {
                    WorkerId = shiftModel.WorkerGuid
                });

                if (lastShiftWorked == null)
                {
                    return BadRequest();
                }

                if (lastShiftWorked != null && 
                    lastShiftWorked.StartDate == shiftModel.StartDate && lastShiftWorked.EndDate == shiftModel.EndDate)
                {
                    return BadRequest("A worker can not work two shifts at the same time");
                }

                if (lastShiftWorked != null &&
                    lastShiftWorked.EndDate == shiftModel.StartDate)
                {
                    return BadRequest("A worker never has two shifts in a row");
                }

                if (lastShiftWorked != null &&
                    lastShiftWorked.EndDate.AddHours(24) > shiftModel.StartDate)
                {
                    return BadRequest("It is a 24 hour timetable");
                }

                return await _mediator.Send(new CreateShiftCommand
                {
                    Shift = _mapper.Map<Shift>(shiftModel)
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        /// <summary>
        ///     Action to retrieve all shift for a worker.
        /// </summary>
        /// <returns>Returns a list of all shift or an empty list, if no shit is set yet</returns>
        /// <response code="200">Returned if the list of shift was retrieved</response>
        /// <response code="400">Returned if the shift could not be retrieved</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet]
        public async Task<ActionResult<List<Shift>>> Shifts(Guid workerGuid)
        {
            try
            {
                return await _mediator.Send(new GetShiftByWorkerGuidQuery
                {
                    WorkerId = workerGuid
                }) ;
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
