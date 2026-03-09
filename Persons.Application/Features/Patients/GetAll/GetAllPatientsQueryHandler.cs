using MediatR;
using Persons.Domain.Common;
using Persons.Domain.Entities;
using Persons.Domain.Interfaces;
using Persons.Domain.Specifications;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Persons.Application.Features.Patients.GetAll
{

    public sealed class GetAllPatientsQueryHandler : IRequestHandler<GetAllPatientsQuery, Result<List<Person>>>
    {
        private readonly IPatientsRepository _patientsRepository;

        public GetAllPatientsQueryHandler(IPatientsRepository patientsRepository)
            => _patientsRepository = patientsRepository;
        public async Task<Result<List<Person>>> Handle(GetAllPatientsQuery request, CancellationToken cancellationToken)
        {
            return Result<List<Person>>.Success(await _patientsRepository.GetAll(new GetPatientsSpecification(request.Ids), cancellationToken));
        }
    }
}