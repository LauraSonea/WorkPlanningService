using MediatR;
using System;
using WorkerApi.Domain.Entities;

namespace WorkerApi.Service.v1.Query
{
    public class GetWorkerByIdQuery : IRequest<Worker>
    {
        public Guid Id { get; set; }
    }
}
