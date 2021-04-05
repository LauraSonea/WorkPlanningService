using System;
using System.Threading;
using System.Threading.Tasks;
using WorkerApi.Domain.Entities;

namespace WorkerApi.Data.Repository.v1
{
    public interface IWorkerRepository: IRepository<Worker>
    {
        Task<Worker> GetWorkerByIdAsync(Guid id, CancellationToken cancellationToken);
    }
}
