using AutoMapper;
using WorkerApi.Domain.Entities;
using WorkerApi.Models.v1;

namespace WorkerApi.Infrastructure.AutoMapper
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateWorkerModel, Worker>().ForMember(x => x.Id, opt => opt.Ignore());

            CreateMap<UpdateWorkerModel, Worker>();
        }
    }
}
