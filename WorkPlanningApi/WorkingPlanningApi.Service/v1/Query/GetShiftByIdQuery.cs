using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkPlanningApi.Domain.Entities;

namespace WorkingPlanningApi.Service.v1.Query
{
    public class GetShiftByIdQuery : IRequest<Shift>
    {
        public Guid Id { get; set; }
    }
}
