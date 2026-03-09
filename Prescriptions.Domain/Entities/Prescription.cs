using Prescriptions.Domain.Enums;
using System;

namespace Prescriptions.Domain.Entities
{
    public class Prescription
    {
        public int Id { get; set; }
        public int AppointmentId { get; set; }
        public int PatientId { get; set; }
        public int DoctorId { get; set; }
        public string Description { get; set; }
        public PrescriptionStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
