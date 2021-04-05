using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WorkPlanningApi.Data.Repository.v1;
using WorkPlanningApi.Domain.Entities;

namespace WorkingPlanningApi.Service.v1.Query
{
    public class GetShiftByWorkerGuidQueryHandler: IRequestHandler<GetShiftByWorkerGuidQuery, List<Shift>>
    {
        private readonly IShiftRepository _shiftRepository;
        public GetShiftByWorkerGuidQueryHandler(IShiftRepository shiftRepository)
        {
            _shiftRepository = shiftRepository;
        }

        public async Task<List<Shift>> Handle(GetShiftByWorkerGuidQuery request, CancellationToken cancellationToken)
        {
            return await _shiftRepository.GetShiftByWorkerGuidAsync(request.WorkerId, cancellationToken);
        }
    }
}
