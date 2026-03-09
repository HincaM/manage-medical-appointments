using FluentValidation;
using System;

namespace Appointments.Application.Features.Appointments.ScheduleAppointment
{
    public class ScheduleAppointmentCommandValidator: AbstractValidator<ScheduleAppointmentCommand>
    {
        public ScheduleAppointmentCommandValidator() 
        {
            RuleFor(x => x.DoctorId)
                .NotEmpty().WithMessage("El id del doctor es requerido")
                .GreaterThan(0).WithMessage("El id del doctor debe ser mayor a 0");
            RuleFor(x => x.PatientId)
                .NotEmpty().WithMessage("El id del paciente es requerido")
                .GreaterThan(0).WithMessage("El id del paciente debe ser mayor a 0");
            RuleFor(x => x.Date)
                .NotEmpty().WithMessage("La fecha de la cita es requerida")
                .Must(date => date > DateTime.Now).WithMessage("La fecha de la cita debe ser en el futuro");
            RuleFor(x => x.Location)
                .NotEmpty().WithMessage("La ubicación de la cita es requerida")
                .MaximumLength(500).WithMessage("La ubicación de la cita no puede tener más de 500 caracteres");
        }
    }
}
