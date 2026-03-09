using FluentValidation;

namespace Persons.Application.Features.Doctors.GetById
{

    internal class GetDoctorByIdQueryValidator : AbstractValidator<GetDoctorByIdQuery>
    {
        public GetDoctorByIdQueryValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0)
                .WithMessage("El id debe ser mayor que 0.");
        }
    }
}