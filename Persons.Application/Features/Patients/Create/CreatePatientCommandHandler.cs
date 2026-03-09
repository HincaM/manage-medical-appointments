using MediatR;
using Persons.Domain.Common;
using Persons.Domain.Entities;
using Persons.Domain.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace Persons.Application.Features.Patients.Create
{

    public sealed class CreatePatientCommandHandler : IRequestHandler<CreatePatientCommand, Result<bool>>
    {
        private readonly IPatientsRepository _patientsRepository;

        public CreatePatientCommandHandler(IPatientsRepository patientsRepository)
            => _patientsRepository = patientsRepository;
        public async Task<Result<bool>> Handle(CreatePatientCommand request, CancellationToken cancellationToken)
        {
            var validator = await new CreatePatientCommandValidator().ValidateAsync(request);
            if (!validator.IsValid) return Result<bool>.Failure(string.Join(", ", validator.Errors));

            var result = await _patientsRepository.Add(new Person
            {
                Identification = request.Identification,
                FirstName = request.FirstName,
                LastName = request.LastName
            }, cancellationToken);

            return result
                ? Result<bool>.Success(true)
                : Result<bool>.Failure("Falló intentando crear paciente");
        }
    }
}