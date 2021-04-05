using MediatR;
using System.Threading;
using System.Threading.Tasks;
using WorkerApi.Data.Repository.v1;
using WorkerApi.Domain.Entities;

namespace WorkerApi.Service.v1.Command
{
    public class CreateWorkerCommandHandler : IRequestHandler<CreateWorkerCommand, Worker>
    {
        private readonly IWorkerRepository _workerRepository;

        public CreateWorkerCommandHandler(IWorkerRepository workerRepository)
        {
            _workerRepository = workerRepository;
        }
        public async Task<Worker> Handle(CreateWorkerCommand request, CancellationToken cancellationToken)
        {
            return await _workerRepository.AddAsync(request.Worker);
        }
    }
}
