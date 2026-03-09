using MediatR;
using Persons.Domain.Common;

namespace Persons.Application.Features.Doctors.Update
{
    public class UpdateDoctorCommand : IRequest<Result<bool>>
    {
        public int Id { get; set; }
        public string Identification { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

    }
}