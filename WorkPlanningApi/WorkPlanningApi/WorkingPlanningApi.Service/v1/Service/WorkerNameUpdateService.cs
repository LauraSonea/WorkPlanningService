using MediatR;
using System;
using System.Diagnostics;
using WorkingPlanningApi.Service.v1.Command;
using WorkingPlanningApi.Service.v1.Models;
using WorkingPlanningApi.Service.v1.Query;

namespace WorkingPlanningApi.Service.v1.Service
{
    public class WorkerNameUpdateService : IWorkerNameUpdateService
    {
        private readonly IMediator _mediator;

        public WorkerNameUpdateService(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async void UpdateWorkerNameInShifts(UpdateWorkerFullNameModel updateWorkerFullNameModel)
        {
            try
            {
                var shiftOfWorker = await _mediator.Send(new GetShiftByWorkerGuidQuery
                {
                    WorkerId = updateWorkerFullNameModel.Id
                });

                if (shiftOfWorker.Count != 0)
                {
                    shiftOfWorker.ForEach(x => x.WorkerFullName = $"{updateWorkerFullNameModel.FirstName} {updateWorkerFullNameModel.LastName}");
                }
                    await _mediator.Send(new UpdateShiftCommand
                {
                    Shifts = shiftOfWorker
                });
            }
            catch (Exception ex)
            {
                // log an error message here

                Debug.WriteLine(ex.Message);
            }
        }
    }
}
