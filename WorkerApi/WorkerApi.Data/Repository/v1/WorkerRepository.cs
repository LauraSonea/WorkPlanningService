using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;
using WorkerApi.Data.Database;
using WorkerApi.Domain.Entities;

namespace WorkerApi.Data.Repository.v1
{
    public class WorkerRepository : Repository<Worker>, IWorkerRepository
    {
        public WorkerRepository(WorkerContext workercontext) : base(workercontext)
        {

        }
        public async Task<Worker> GetWorkerByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await WorkerContext.Worker.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }
    }
}
