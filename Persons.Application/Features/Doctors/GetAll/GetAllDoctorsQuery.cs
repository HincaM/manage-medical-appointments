using MediatR;
using Persons.Domain.Common;
using Persons.Domain.Entities;
using System.Collections.Generic;

namespace Persons.Application.Features.Doctors.GetAll
{
    public class GetAllDoctorsQuery : IRequest<Result<List<Person>>> 
    {
        public List<int> Ids { get; set; }
    }

}