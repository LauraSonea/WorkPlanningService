using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkerApi.Domain.Entities;
using WorkerApi.Models.v1;
using WorkerApi.Service.v1.Command;
using WorkerApi.Service.v1.Query;

namespace WorkerApi.Controllers.v1
{
    [Produces("application/json")]
    [Route("v1/[controller]")]
    [ApiController]
    public class WorkerController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public WorkerController(IMapper mapper, IMediator mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        /// <summary>
        /// Action to create a new worker in the database.
        /// </summary>
        /// <param name="createWorkerModel">Model to create a new worker</param>
        /// <returns>Returns the created worker</returns>
        /// <response code="200">Returned if the worker was created</response>
        /// <response code="400">Returned if the model couldn't be parsed or the worker couldn't be saved</response>
        /// <response code="422">Returned when the validation failed</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [HttpPost]
        public async Task<ActionResult<Worker>> Worker(CreateWorkerModel createWorkerModel)
        {
            try
            {
                return await _mediator.Send(new CreateWorkerCommand
                {
                    Worker = _mapper.Map<Worker>(createWorkerModel)
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Action to update an existing worker
        /// </summary>
        /// <param name="updateWorkerModel"></param>
        /// <returns>Returns the updated worker</returns>
        /// <response code="200">Returned if the worker was updated</response>
        /// <response code="400">Returned if the model couldn't be parsed or the worker couldn't be found</response>
        /// <response code="422">Returned when the validation failed</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [HttpPut]
        public async Task<ActionResult<Worker>> Worker(UpdateWorkerModel updateWorkerModel)
        {
            try
            {
                var worker = await _mediator.Send(new GetWorkerByIdQuery
                {
                    Id = updateWorkerModel.Id
                });

                if (worker == null)
                {
                    return BadRequest($"No worker found with the id {updateWorkerModel.Id}");
                }

                return await _mediator.Send(new UpdateWorkerCommand
                {
                    Worker = _mapper.Map(updateWorkerModel, worker)
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
