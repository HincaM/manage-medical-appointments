using MediatR;
using Persons.Domain.Common;

namespace Persons.Application.Features.Patients.Delete
{

    public class DeletePatientCommand : IRequest<Result<bool>>
    {
        public int Id { get; set; }

    }

}