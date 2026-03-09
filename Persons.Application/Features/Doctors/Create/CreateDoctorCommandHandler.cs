using MediatR;
using Persons.Domain.Common;
using Persons.Domain.Entities;
using Persons.Domain.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace Persons.Application.Features.Doctors.Create
{
    public sealed class CreateDoctorCommandHandler : IRequestHandler<CreateDoctorCommand, Result<bool>>
    {
        private readonly IDoctorsRepository _doctorsRepository;

        public CreateDoctorCommandHandler(IDoctorsRepository doctorsRepository)
            => _doctorsRepository = doctorsRepository;
        public async Task<Result<bool>> Handle(CreateDoctorCommand request, CancellationToken cancellationToken)
        {
            var validator = await new CreateDoctorCommandValidator().ValidateAsync(request);
            if (!validator.IsValid) return Result<bool>.Failure(string.Join(", ", validator.Errors));

            var result = await _doctorsRepository.Add(new Person
            {
                Identification = request.Identification,
                FirstName = request.FirstName,
                LastName = request.LastName
            }, cancellationToken);

            return result
                ? Result<bool>.Success(true)
                : Result<bool>.Failure("Falló intentando crear médico");

        }
    }

}