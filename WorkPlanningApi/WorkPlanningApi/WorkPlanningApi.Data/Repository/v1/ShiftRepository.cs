using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WorkPlanningApi.Data.Database;
using WorkPlanningApi.Domain.Entities;

namespace WorkPlanningApi.Data.Repository.v1
{
    public class ShiftRepository : Repository<Shift>, IShiftRepository
    {
        public ShiftRepository(ShiftContext shiftContext): base(shiftContext)
        {

        }

        public async Task<Shift> GetShiftByIdAsync(Guid shiftId, CancellationToken cancellationToken)
        {
            return await ShiftContext.Shift.FirstOrDefaultAsync(x => x.Id == shiftId, cancellationToken);
        }

        public async Task<Shift> GetLastWorkedShiftByWorkerIdAsync(Guid workerId, CancellationToken cancellationToken)
        {
            return await ShiftContext.Shift.Where(x => x.WorkerGuid == workerId).OrderByDescending(x => x.StartDate).FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<List<Shift>> GetShiftByWorkerGuidAsync(Guid workerId, CancellationToken cancellationToken)
        {
            return await ShiftContext.Shift.Where(x => x.WorkerGuid == workerId).ToListAsync(cancellationToken);
        }
    }
}
