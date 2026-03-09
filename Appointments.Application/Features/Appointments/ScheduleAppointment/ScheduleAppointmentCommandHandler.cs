using Appointments.Domain.Entities;
using Appointments.Domain.Interfaces;
using Appointments.Domain.Common;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Appointments.Application.Features.Appointments.ScheduleAppointment
{
    public sealed class ScheduleAppointmentCommandHandler : IRequestHandler<ScheduleAppointmentCommand, Result<bool>>
    {
        private readonly IAppointmentRepository _appointmentRepository;

        public ScheduleAppointmentCommandHandler(IAppointmentRepository appointmentRepository)
        {
            _appointmentRepository = appointmentRepository;
        }

        public async Task<Result<bool>> Handle(ScheduleAppointmentCommand request, CancellationToken cancellationToken)
        {
            var validator = await new ScheduleAppointmentCommandValidator().ValidateAsync(request);
            if (!validator.IsValid) return Result<bool>.Failure(string.Join(", ", validator.Errors));

            var result = await _appointmentRepository.Schedule(new Appointment
            {
                Date = request.Date,
                Location = request.Location,
                PatientId = request.PatientId,
                DoctorId = request.DoctorId
            }, cancellationToken);

            if (result)
                return Result<bool>.Success(true);
            else
                return Result<bool>.Failure("Falló al agendar cita.");
        }
    }
}
