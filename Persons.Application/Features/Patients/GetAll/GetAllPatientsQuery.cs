using MediatR;
using Persons.Domain.Common;
using Persons.Domain.Entities;
using System.Collections.Generic;

namespace Persons.Application.Features.Patients.GetAll
{
    public class GetAllPatientsQuery : IRequest<Result<List<Person>>>     
    {
        public List<int> Ids { get; set; }
    }

}