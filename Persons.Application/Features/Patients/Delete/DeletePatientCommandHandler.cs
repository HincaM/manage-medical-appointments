using MediatR;
using Persons.Domain.Common;
using Persons.Domain.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace Persons.Application.Features.Patients.Delete
{
    public sealed class DeletePatientCommandHandler : IRequestHandler<DeletePatientCommand, Result<bool>>
    {
        private readonly IPatientsRepository _patientsRepository;

        public DeletePatientCommandHandler(IPatientsRepository patientsRepository)
        {
            _patientsRepository = patientsRepository;
        }
        public async Task<Result<bool>> Handle(DeletePatientCommand request, CancellationToken cancellationToken)
        {
            var validator = await new DeletePatientCommandValidator().ValidateAsync(request);
            if (!validator.IsValid) return Result<bool>.Failure(string.Join(", ", validator.Errors));

            var result = await _patientsRepository.Delete(request.Id, cancellationToken);
            return result
                ? Result<bool>.Success(true)
                : Result<bool>.Failure("Falló intentando eliminar paciente");
        }
    }
}
