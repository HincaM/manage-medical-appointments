using Prescriptions.Domain.Entities;
using Prescriptions.Domain.Interfaces;
using System.Collections.Generic;

namespace Prescriptions.Application.Specifications
{
    public class GetPrescriptionsSpecification: Specification<Prescription>
    {
        public GetPrescriptionsSpecification(List<int> ids)
        {
            Criteria = p => ids != null && ids.Count > 0 ? ids.Contains(p.Id) : true;
        }
    }
}
