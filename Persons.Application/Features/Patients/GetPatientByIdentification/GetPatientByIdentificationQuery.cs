using MediatR;
using Persons.Domain.Common;
using Persons.Domain.Entities;

namespace Persons.Application.Features.Patients.GetPatientByIdentification
{

    public class GetPatientByIdentificationQuery : IRequest<Result<Person>>
    {
                
        public string Identification { get; set; }
    }
}