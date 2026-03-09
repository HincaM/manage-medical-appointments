using FluentValidation;

namespace Persons.Application.Features.Patients.GetPatientByIdentification
{

    internal class GetPatientByIdentificationQueryValidator : AbstractValidator<GetPatientByIdentificationQuery>
    {
        public GetPatientByIdentificationQueryValidator()
        {
            RuleFor(x => x.Identification)
                .NotNull()
                .WithMessage("El id no puede ser nulo.")
                .NotEmpty()
                .WithMessage("El id no puede estar vacío.");
        }
    }
}