using MediatR;
using Persons.Domain.Common;
using Persons.Domain.Entities;
using Persons.Domain.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace Persons.Application.Features.Patients.Update
{

    public class UpdatePatientCommandHandler : IRequestHandler<UpdatePatientCommand, Result<bool>>
    {
        private readonly IPatientsRepository _patientsRepository;

        public UpdatePatientCommandHandler(IPatientsRepository patientsRepository)
            => _patientsRepository = patientsRepository;
        public async Task<Result<bool>> Handle(UpdatePatientCommand request, CancellationToken cancellationToken)
        {
            var validator = await new UpdatePatientCommandValidator().ValidateAsync(request);
            if (!validator.IsValid) return Result<bool>.Failure(string.Join(", ", validator.Errors));

            var result = await _patientsRepository.Update(new Person
            {
                Id = request.Id,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Identification = request.Identification
            }, cancellationToken);

            return result
                ? Result<bool>.Success(true)
                : Result<bool>.Failure("Falló intentando actualizar paciente");

        }
    }
}