using Appointments.Application.Interfaces;
using Appointments.Domain.Common;
using Appointments.Domain.Events;
using Appointments.Domain.Interfaces;
using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Appointments.Application.Features.Appointments.FinishAppointment
{
    public sealed class FinishAppointmentCommandHandler : IRequestHandler<FinishAppointmentCommand, Result<bool>>
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IEventBus _eventBus;

        public FinishAppointmentCommandHandler(
            IAppointmentRepository appointmentRepository,
            IEventBus eventBus)
        {
            _appointmentRepository = appointmentRepository;
            _eventBus = eventBus;
        }

        public async Task<Result<bool>> Handle(FinishAppointmentCommand request, CancellationToken cancellationToken)
        {
            
            var validator = await new FinishAppointmentCommandValidator().ValidateAsync(request);
            if (!validator.IsValid) return Result<bool>.Failure(string.Join(", ", validator.Errors));

            if (await _appointmentRepository.Finish(request.AppointmentId, cancellationToken))
            {
                _eventBus.Publish(new AppointmentFinishedEvent
                {
                    AppointmentId = request.AppointmentId,
                    Queue = "prescriptions",
                    Description = request.Description,
                    DoctorId = request.DoctorId,
                    PatientId = request.PatientId
                });

                return Result<bool>.Success(true);
            }

            return Result<bool>.Failure("No se logró finalizar la cita");
        }
    }
}
