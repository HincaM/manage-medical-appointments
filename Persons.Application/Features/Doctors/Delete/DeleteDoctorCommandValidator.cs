using FluentValidation;

namespace Persons.Application.Features.Doctors.Delete
{
    internal sealed class DeleteDoctorCommandValidator : AbstractValidator<DeleteDoctorCommand>
    {
        public DeleteDoctorCommandValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("El id debe ser mayor a cero.");
        }
    }
}
