using MediatR;
using System.Threading;
using System.Threading.Tasks;
using WorkerApi.Data.Repository.v1;
using WorkerApi.Domain.Entities;
using WorkerApi.Messaging.Send.Sender.v1;

namespace WorkerApi.Service.v1.Command
{
    public class UpdateWorkerCommandHandler: IRequestHandler<UpdateWorkerCommand, Worker>
    {
        private readonly IWorkerRepository _workerRepository;
        private readonly IWorkerUpdateSender _workerUpdateSender;

        public UpdateWorkerCommandHandler(IWorkerRepository workerRepository, IWorkerUpdateSender workerUpdateSender )
        {
            _workerRepository = workerRepository;
            _workerUpdateSender = workerUpdateSender;
        }

        public async Task<Worker> Handle(UpdateWorkerCommand request, CancellationToken cancellationToken)
        {
            var worker = await _workerRepository.UpdateAsync(request.Worker);

            _workerUpdateSender.SendWorker(worker);

            return worker;
        }
    }
}
