using MediatR;
using System.Threading;
using System.Threading.Tasks;
using WorkPlanningApi.Data.Repository.v1;

namespace WorkingPlanningApi.Service.v1.Command
{
    public class UpdateShiftCommandHandler : IRequestHandler<UpdateShiftCommand>
    {
        private readonly IShiftRepository _shiftRepository;
        public UpdateShiftCommandHandler(IShiftRepository shiftRepository)
        {
            _shiftRepository = shiftRepository;
        }
        public async Task<Unit> Handle(UpdateShiftCommand request, CancellationToken cancellationToken)
        {
            await _shiftRepository.UpdateRangeAsync(request.Shifts);

            return Unit.Value;
        }
    }
}
