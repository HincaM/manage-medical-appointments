using Prescriptions.Domain.Entities;
using Prescriptions.Domain.Interfaces;

namespace Prescriptions.Application.Specifications
{
    public class GetByPatientIdSpecification: Specification<Prescription>
    {
        public GetByPatientIdSpecification(int patientId)
        {
            Criteria = p => p.PatientId == patientId;
        }
    }
}
