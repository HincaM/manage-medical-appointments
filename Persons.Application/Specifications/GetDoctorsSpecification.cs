using System.Collections.Generic;

namespace Persons.Domain.Specifications
{
    public class GetDoctorsSpecification: DoctorSpecificationBase
    {
        public GetDoctorsSpecification(List<int> ids)
        {
            Criteria = p => p.Type == Doctor && 
            (ids != null && ids.Count > 0 ? ids.Contains(p.Id) : true);
        }
    }
}
