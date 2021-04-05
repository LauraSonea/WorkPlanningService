using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WorkPlanningApi.Domain.Entities;

namespace WorkPlanningApi.Data.Repository.v1
{
    public interface IShiftRepository : IRepository<Shift> 
    {
        Task<Shift> GetShiftByIdAsync(Guid shiftId, CancellationToken cancellationToken);

        Task<List<Shift>> GetShiftByWorkerGuidAsync(Guid workerId, CancellationToken cancellationToken);

        Task<Shift> GetLastWorkedShiftByWorkerIdAsync(Guid workerId, CancellationToken cancellationToken);
    }
}
