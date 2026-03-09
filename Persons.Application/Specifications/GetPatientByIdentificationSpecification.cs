using Persons.Domain.Interfaces;

namespace Persons.Domain.Specifications
{
    public class GetPatientByIdentificationSpecification: PatientSpecificationBase
    {
        public GetPatientByIdentificationSpecification(string identification) 
        {
            Criteria = p => p.Type == Patient && p.Identification == identification;
        }
    }
}
