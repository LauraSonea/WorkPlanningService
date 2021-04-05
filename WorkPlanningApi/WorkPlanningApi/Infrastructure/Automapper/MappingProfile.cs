using AutoMapper;
using WorkPlanningApi.Domain.Entities;
using WorkPlanningApi.Models.v1;

namespace WorkPlanningApi.Infrastructure.Automapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ShiftModel, Shift>();
        }
    }
}
