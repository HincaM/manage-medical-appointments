using MediatR;
using Persons.Domain.Common;
using Persons.Domain.Entities;
using Persons.Domain.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace Persons.Application.Features.Doctors.Update
{

    public class UpdateDoctorCommandHandler : IRequestHandler<UpdateDoctorCommand, Result<bool>>
    {
        private readonly IDoctorsRepository _doctorsRepository;

        public UpdateDoctorCommandHandler(IDoctorsRepository doctorsRepository)
        {
            _doctorsRepository = doctorsRepository;
        }
        public async Task<Result<bool>> Handle(UpdateDoctorCommand request, CancellationToken cancellationToken)
        {
            var validator = await new UpdateDoctorCommandValidator().ValidateAsync(request);
            if (!validator.IsValid) return Result<bool>.Failure(string.Join(", ", validator.Errors));

            var result = await _doctorsRepository.Update(new Person 
            { 
                Id = request.Id, 
                FirstName = request.FirstName, 
                LastName = request.LastName,
                Identification = request.Identification
            }, cancellationToken);

            return result 
                ? Result<bool>.Success(true)
                : Result<bool>.Failure("Falló intentando actualiza médico");

        }
    }
}