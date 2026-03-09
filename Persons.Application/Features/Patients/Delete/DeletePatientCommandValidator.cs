using FluentValidation;

namespace Persons.Application.Features.Patients.Delete
{
    internal sealed class DeletePatientCommandValidator : AbstractValidator<DeletePatientCommand>
    {
        public DeletePatientCommandValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("El id debe ser mayor a cero.");
        }
    }
}
