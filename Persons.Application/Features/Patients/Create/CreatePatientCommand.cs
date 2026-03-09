using MediatR;
using Persons.Domain.Common;

namespace Persons.Application.Features.Patients.Create
{

    public class CreatePatientCommand
         : IRequest<Result<bool>>
    {

        public string Identification { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}