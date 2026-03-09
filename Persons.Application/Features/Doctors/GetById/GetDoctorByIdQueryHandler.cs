using MediatR;
using Persons.Domain.Common;
using Persons.Domain.Entities;
using Persons.Domain.Interfaces;
using Persons.Domain.Specifications;
using System.Threading;
using System.Threading.Tasks;

namespace Persons.Application.Features.Doctors.GetById
{
    public sealed class GetDoctorByIdQueryHandler : IRequestHandler<GetDoctorByIdQuery, Result<Person>>
    {
        private readonly IDoctorsRepository _doctorsRepository;

        public GetDoctorByIdQueryHandler(IDoctorsRepository doctorsRepository)
            => _doctorsRepository = doctorsRepository;
        public async Task<Result<Person>> Handle(GetDoctorByIdQuery request, CancellationToken cancellationToken)
        {
            var result = new GetDoctorByIdQueryValidator().Validate(request);
            if (!result.IsValid) return Result<Person>.Failure(string.Join(", ", result.Errors));

            return Result<Person>.Success(await _doctorsRepository.Get(new GetDoctorSpecification(request.Id), cancellationToken));

        }
    }
}