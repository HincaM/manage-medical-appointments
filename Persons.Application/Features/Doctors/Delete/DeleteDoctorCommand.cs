using MediatR;
using Persons.Domain.Common;

namespace Persons.Application.Features.Doctors.Delete
{
    public class DeleteDoctorCommand : IRequest<Result<bool>>
    {
        public int Id { get; set; }
    }
}