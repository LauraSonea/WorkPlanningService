using MediatR;
using System.Collections.Generic;
using WorkPlanningApi.Domain.Entities;

namespace WorkingPlanningApi.Service.v1.Command
{
    public class UpdateShiftCommand: IRequest
    {
        public List<Shift> Shifts { get; set; }
    }
}
