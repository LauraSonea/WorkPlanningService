using WorkPlanningApi.Domain.Entities;

namespace WorkPlanningApi.Messaging.Send.Sender.v1
{
   public interface IShiftUpdateSender
    {
        void SendShift(Shift shift);
    }
}