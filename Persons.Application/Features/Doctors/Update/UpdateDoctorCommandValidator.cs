using FluentValidation;

namespace Persons.Application.Features.Doctors.Update
{

    internal class UpdateDoctorCommandValidator : AbstractValidator<UpdateDoctorCommand>
    {
        public UpdateDoctorCommandValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("El id debe ser mayor a cero.");
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("El nombre es requerido.")
                .MaximumLength(100).WithMessage("El nombre no puede tener más de 100 caracteres.");
            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("El apellido es requerido.")
                .MaximumLength(100).WithMessage("El apellido no puede tener más de 100 caracteres.");
        }
    }
}