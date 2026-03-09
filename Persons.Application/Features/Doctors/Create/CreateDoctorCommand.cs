using MediatR;
using Persons.Domain.Common;

namespace Persons.Application.Features.Doctors.Create
{
    public class CreateDoctorCommand : IRequest<Result<bool>>
    {
        public string Identification { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

    }
}
