using Appointments.Domain.Common;
using MediatR;
using System;

namespace Appointments.Application.Features.Appointments.ScheduleAppointment
{
    public class ScheduleAppointmentCommand : IRequest<Result<bool>>
    {
        public DateTime Date { get; set; }
        public string Location { get; set; }
        public int PatientId { get; set; }
        public int DoctorId { get; set; }
    }
}