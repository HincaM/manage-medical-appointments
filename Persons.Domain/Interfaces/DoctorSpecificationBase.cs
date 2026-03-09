using Persons.Domain.Entities;
using Persons.Domain.Enums;
using Persons.Domain.Interfaces;

namespace Persons.Domain.Specifications
{
    public class DoctorSpecificationBase : Specification<Person>
    {
        protected PersonType Doctor = PersonType.Doctor;
    }
}
