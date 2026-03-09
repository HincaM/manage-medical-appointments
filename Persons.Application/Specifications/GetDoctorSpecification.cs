namespace Persons.Domain.Specifications
{
    public class GetDoctorSpecification: DoctorSpecificationBase
    {
        public GetDoctorSpecification(int id)
        {
            Criteria = p => p.Type == Doctor && p.Id == id;
        }
    }

    
}


