using MediatR;
using Persons.Domain.Common;

namespace Persons.Application.Features.Patients.Update
{

    public class UpdatePatientCommand
         : IRequest<Result<bool>>
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Identification { get; set; }
    }
}