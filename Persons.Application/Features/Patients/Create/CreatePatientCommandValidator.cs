using FluentValidation;

namespace Persons.Application.Features.Patients.Create
{
    public class CreatePatientCommandValidator : AbstractValidator<CreatePatientCommand>
    {
        public CreatePatientCommandValidator()
        {
            RuleFor(x => x.Identification).NotEmpty().WithMessage("Identificación es requerida.");
            RuleFor(x => x.FirstName).NotEmpty().WithMessage("Nombre es requerido.");
            RuleFor(x => x.LastName).NotEmpty().WithMessage("Apellido es requerido.");
        }
    }
}
