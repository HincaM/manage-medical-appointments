using FluentValidation;

namespace Persons.Application.Features.Doctors.Create
{
    public class CreateDoctorCommandValidator: AbstractValidator<CreateDoctorCommand>
    {
        public CreateDoctorCommandValidator()
        {
            RuleFor(x => x.Identification).NotEmpty().WithMessage("Identificación es requerida.");
            RuleFor(x => x.FirstName).NotEmpty().WithMessage("Nombre es requerido.");
            RuleFor(x => x.LastName).NotEmpty().WithMessage("Apellido es requerido.");
        }
    }
}
