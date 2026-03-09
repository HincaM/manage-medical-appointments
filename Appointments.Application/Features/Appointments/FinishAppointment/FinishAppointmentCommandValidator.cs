using FluentValidation;

namespace Appointments.Application.Features.Appointments.FinishAppointment
{
    public class FinishAppointmentCommandValidator: AbstractValidator<FinishAppointmentCommand>
    {
        public FinishAppointmentCommandValidator()
        {
            RuleFor(x => x.AppointmentId)
                .GreaterThan(0)
                .WithMessage("El id de cita deber ser mayor a cero.");
            RuleFor(x => x.PatientId)
                .GreaterThan(0)
                .WithMessage("El id del paciente debe ser mayor a cero.");
            RuleFor(x => x.DoctorId)
                .GreaterThan(0)
                .WithMessage("El id del doctor debe ser mayor a cero.");
            RuleFor(x => x.Description)
                .NotEmpty()
                .WithMessage("La descripción no debe estar vacía.");
        }
    }
}
