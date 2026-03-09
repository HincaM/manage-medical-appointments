using MediatR;
using Prescriptions.Domain.Common;

namespace Prescriptions.Application.Features.Prescriptions.Create
{
    public class CreatePrescriptionCommand: IRequest<Result<bool>>
    {
        public int AppointmentId { get; set; }
        public int PatientId { get; set; }
        public int DoctorId { get; set; }
        public string Description { get; set; }
    }
}
