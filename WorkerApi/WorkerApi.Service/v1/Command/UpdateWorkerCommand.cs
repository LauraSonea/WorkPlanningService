using MediatR;
using WorkerApi.Domain.Entities;

namespace WorkerApi.Service.v1.Command
{
    public class UpdateWorkerCommand: IRequest<Worker>
    {
        public Worker Worker { get; set; }

    }
}
