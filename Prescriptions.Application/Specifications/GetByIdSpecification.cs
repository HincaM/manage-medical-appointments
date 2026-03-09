using Prescriptions.Domain.Entities;
using Prescriptions.Domain.Interfaces;

namespace Prescriptions.Application.Specifications
{
    public class GetByIdSpecification: Specification<Prescription>
    {
        public GetByIdSpecification(int id) 
        {
            Criteria = p => p.Id == id;
        }
    }
}
