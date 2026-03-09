namespace Prescriptions.Application.Features.Prescriptions.GetByPatientIdentification
{
    public class PatientDto
    {
        public PatientDto() { }
        public int Id { get; set; }
        public string Identification { get; set; } = string.Empty;
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
