using MediatR;
using WorkPlanningApi.Domain.Entities;

namespace WorkingPlanningApi.Service.v1.Command
{
    public class CreateShiftCommand : IRequest<Shift>
    {
        public Shift Shift { get; set; }
    }
}
