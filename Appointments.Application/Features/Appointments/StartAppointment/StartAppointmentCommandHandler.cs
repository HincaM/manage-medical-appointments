using Appointments.Domain.Interfaces;
using Appointments.Domain.Common;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Appointments.Application.Features.Appointments.StartAppointment
{
    public sealed class StartAppointmentCommandHandler : IRequestHandler<StartAppointmentCommand, Result<bool>>
    {
        private readonly IAppointmentRepository _appointmentRepository;

        public StartAppointmentCommandHandler(IAppointmentRepository appointmentRepository)
        {
            _appointmentRepository = appointmentRepository;
        }

        public async Task<Result<bool>> Handle(StartAppointmentCommand request, CancellationToken cancellationToken) 
        {
            var result = await _appointmentRepository.Start(request.AppointmentId, cancellationToken);

            if (result)
                return Result<bool>.Success(true);
            else
                return Result<bool>.Failure("Falló al iniciar la cita.");
        }
    }
}
