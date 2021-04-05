using WorkPlanningApi.Domain.Entities;

namespace WorkPlanningApi.Messaging.Receiver.v1
{
   public interface IShiftUpdateSender
    {
        void SendShift(Shift shift);
    }
}