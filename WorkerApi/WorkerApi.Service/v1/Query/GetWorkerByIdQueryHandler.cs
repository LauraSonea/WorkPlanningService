using MediatR;
using System.Threading;
using System.Threading.Tasks;
using WorkerApi.Data.Repository.v1;
using WorkerApi.Domain.Entities;

namespace WorkerApi.Service.v1.Query
{
    public class GetWorkerByIdQueryHandler : IRequestHandler<GetWorkerByIdQuery, Worker>
    {
        private readonly IWorkerRepository _workerRepository;

        public GetWorkerByIdQueryHandler(IWorkerRepository workerRepository)
        {
            _workerRepository = workerRepository;
        }   
        public async Task<Worker> Handle(GetWorkerByIdQuery request, CancellationToken cancellationToken)
        {
            return await _workerRepository.GetWorkerByIdAsync(request.Id, cancellationToken);
        }
    }
}
