using WorkerApi.Domain.Entities;

namespace WorkerApi.Messaging.Send.Sender.v1
{
    public interface IWorkerUpdateSender
    {
        void SendWorker(Worker worker);

    }
}
