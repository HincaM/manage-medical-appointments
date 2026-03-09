using Appointments.Domain.Common;
using MediatR;

namespace Appointments.Application.Features.Appointments.FinishAppointment
{
    public class FinishAppointmentCommand : IRequest<Result<bool>>
    {
        public int AppointmentId { get; set; }
        public int PatientId { get; set; }
        public int DoctorId { get; set; }
        public string Description { get; set; }
    }
}
    
