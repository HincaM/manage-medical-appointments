namespace Appointments.Application.Interfaces
{
    public abstract class IEvent
    {
        public string Queue { get; set; }
    }
}
