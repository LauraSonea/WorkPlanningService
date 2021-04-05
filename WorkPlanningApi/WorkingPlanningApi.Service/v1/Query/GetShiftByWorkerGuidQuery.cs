using MediatR;
using System;
using System.Collections.Generic;
using WorkPlanningApi.Domain.Entities;

namespace WorkingPlanningApi.Service.v1.Query
{
    public class GetShiftByWorkerGuidQuery: IRequest<List<Shift>>
    {
        public Guid WorkerId { get; set; }
    }
}