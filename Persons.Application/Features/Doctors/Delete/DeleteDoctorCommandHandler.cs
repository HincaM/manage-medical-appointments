using MediatR;
using Persons.Domain.Common;
using Persons.Domain.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace Persons.Application.Features.Doctors.Delete
{
    public sealed class DeleteDoctorCommandHandler : IRequestHandler<DeleteDoctorCommand, Result<bool>>
    {
        private readonly IDoctorsRepository _doctorsRepository;

        public DeleteDoctorCommandHandler(IDoctorsRepository doctorsRepository)
            => _doctorsRepository = doctorsRepository;
        public async Task<Result<bool>> Handle(DeleteDoctorCommand request, CancellationToken cancellationToken)
        {
            var validator = await new DeleteDoctorCommandValidator().ValidateAsync(request);
            if (!validator.IsValid) return Result<bool>.Failure(string.Join(", ", validator.Errors));

            var result = await _doctorsRepository.Delete(request.Id, cancellationToken);

            return result ? Result<bool>.Success(true) :
                Result<bool>.Failure("Falló intentando eliminar médico");
        }
    }
}
