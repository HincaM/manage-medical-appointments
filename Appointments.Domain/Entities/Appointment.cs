using Appointments.Domain.Enums;
using System;

namespace Appointments.Domain.Entities
{
    public class Appointment
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Location { get; set; }
        public int PatientId { get; set; }
        public int DoctorId { get; set; }
        public AppointmentStatus Status { get; set; }
    }
}
