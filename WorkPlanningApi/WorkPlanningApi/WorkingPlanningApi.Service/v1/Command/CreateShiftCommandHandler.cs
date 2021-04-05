using MediatR;
using System.Threading;
using System.Threading.Tasks;
using WorkPlanningApi.Data.Repository.v1;
using WorkPlanningApi.Domain.Entities;

namespace WorkingPlanningApi.Service.v1.Command
{
    public class CreateShiftCommandHandler : IRequestHandler<CreateShiftCommand, Shift>
    {
        private readonly IShiftRepository _shiftRepository;

        public CreateShiftCommandHandler(IShiftRepository shiftRepository)
        {
            _shiftRepository = shiftRepository;
        }
        public async Task<Shift> Handle(CreateShiftCommand request, CancellationToken cancellationToken)
        {
            return await _shiftRepository.AddAsync(request.Shift);
        }
    }
}
