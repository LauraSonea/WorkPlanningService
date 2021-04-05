using MediatR;
using System.Threading;
using System.Threading.Tasks;
using WorkPlanningApi.Data.Repository.v1;
using WorkPlanningApi.Domain.Entities;

namespace WorkingPlanningApi.Service.v1.Query
{
    public class GetShiftByIdQueryHandler : IRequestHandler<GetShiftByIdQuery, Shift>
    {
        private readonly IShiftRepository _shiftRepository;

        public GetShiftByIdQueryHandler(IShiftRepository shiftRepository)
        {
            _shiftRepository = shiftRepository;
        }

        public async Task<Shift> Handle(GetShiftByIdQuery request, CancellationToken cancellationToken)
        {
            return await _shiftRepository.GetShiftByIdAsync(request.Id, cancellationToken);
        }
    }
}
