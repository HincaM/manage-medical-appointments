using Persons.Domain.Interfaces;
using System.Collections.Generic;

namespace Persons.Domain.Specifications
{
    public class GetPatientsSpecification: PatientSpecificationBase
    {
        public GetPatientsSpecification(List<int> ids) 
        {
            Criteria = p => p.Type == Domain.Enums.PersonType.Patient && 
            (ids != null && ids.Count > 0 ? ids.Contains(p.Id) : true);
        }
    }
}
