namespace Appointments.Application.Interfaces
{
    public interface IEventBus
    {
        void Publish<TEvent>(TEvent @event) where TEvent : IEvent;
    }
}
