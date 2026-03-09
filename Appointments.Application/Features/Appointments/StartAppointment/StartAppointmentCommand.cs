using Appointments.Domain.Common;
using MediatR;

namespace Appointments.Application.Features.Appointments.StartAppointment
{
    public class StartAppointmentCommand : IRequest<Result<bool>>
    {
        public int AppointmentId { get; set; }
    }

}