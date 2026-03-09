using MediatR;
using Persons.Domain.Common;
using Persons.Domain.Entities;

namespace Persons.Application.Features.Doctors.GetById {

    public class GetDoctorByIdQuery : IRequest<Result<Person>>
    {
        public int Id { get; set; }
    }
}
