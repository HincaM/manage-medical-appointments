using Appointments.Application.Interfaces;

namespace Appointments.Domain.Events
{
    public class AppointmentFinishedEvent : IEvent
    {
        public int AppointmentId { get; set; }
        public int PatientId { get; set; }
        public int DoctorId { get; set; }
        public string Description { get; set; }
    }
}
