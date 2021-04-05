using MediatR;
using System.Threading;
using System.Threading.Tasks;
using WorkPlanningApi.Data.Repository.v1;
using WorkPlanningApi.Domain.Entities;

namespace WorkingPlanningApi.Service.v1.Query
{
    public class GetLastWorkedShiftByWorkerIdQueryHandler : IRequestHandler<GetLastWorkedShiftByWorkerIdQuery, Shift>
    {
        private readonly IShiftRepository _shiftRepository;
        public GetLastWorkedShiftByWorkerIdQueryHandler(IShiftRepository shiftRepository)
        {
            _shiftRepository = shiftRepository;
        }

        public async Task<Shift> Handle(GetLastWorkedShiftByWorkerIdQuery request, CancellationToken cancellationToken)
        {
            return await _shiftRepository.GetLastWorkedShiftByWorkerIdAsync(request.WorkerId, cancellationToken);
        }
    }
}
