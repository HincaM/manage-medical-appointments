using MediatR;
using Persons.Domain.Common;
using Persons.Domain.Entities;
using Persons.Domain.Interfaces;
using Persons.Domain.Specifications;
using System.Threading;
using System.Threading.Tasks;

namespace Persons.Application.Features.Patients.GetPatientByIdentification
{

    public sealed class GetPatientByIdentificationQueryHandler : IRequestHandler<GetPatientByIdentificationQuery, Result<Person>>
    {
        private readonly IPatientsRepository _patientsRepository;

        public GetPatientByIdentificationQueryHandler(IPatientsRepository patientsRepository)
        {
            _patientsRepository = patientsRepository;
        }
        public async Task<Result<Person>> Handle(GetPatientByIdentificationQuery request, CancellationToken cancellationToken)
        {
            var result = new GetPatientByIdentificationQueryValidator().Validate(request);
            if (!result.IsValid) return Result<Person>.Failure(string.Join(", ", result.Errors));

            return Result<Person>.Success(await _patientsRepository.Get(new GetPatientByIdentificationSpecification(request.Identification), cancellationToken));
        }
    }
}