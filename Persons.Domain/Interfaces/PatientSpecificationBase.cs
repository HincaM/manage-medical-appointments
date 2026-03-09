using Persons.Domain.Entities;
using Persons.Domain.Enums;

namespace Persons.Domain.Interfaces
{
    public abstract class PatientSpecificationBase : Specification<Person>
    {
        public PersonType Patient = PersonType.Patient;

    }
}
