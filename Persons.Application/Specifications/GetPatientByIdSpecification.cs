using Persons.Domain.Entities;
using Persons.Domain.Enums;
using Persons.Domain.Interfaces;

namespace Persons.Domain.Specifications
{
    public class GetPatientByIdSpecification: Specification<Person>
    {
        public GetPatientByIdSpecification(int id) 
        {
            Criteria = p => p.Type == PersonType.Patient && p.Id == id;
        }
    }
}
