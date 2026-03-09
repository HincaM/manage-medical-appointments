using MediatR;
using Persons.Domain.Common;
using Persons.Domain.Entities;
using Persons.Domain.Interfaces;
using Persons.Domain.Specifications;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Persons.Application.Features.Doctors.GetAll
{

    public sealed class GetAllDoctorsQueryHandler : IRequestHandler<GetAllDoctorsQuery, Result<List<Person>>>
    {
        private readonly IDoctorsRepository _doctorsRepository;

        public GetAllDoctorsQueryHandler(IDoctorsRepository doctorsRepository)
            => _doctorsRepository = doctorsRepository;
        public async Task<Result<List<Person>>> Handle(GetAllDoctorsQuery request, CancellationToken cancellationToken)
        {
            var result = await _doctorsRepository.GetAll(new GetDoctorsSpecification(request.Ids), cancellationToken);

            return Result<List<Person>>.Success(result);
        }
    }
}