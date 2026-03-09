using MediatR;
using Prescriptions.Domain.Common;
using Prescriptions.Domain.Entities;
using Prescriptions.Domain.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace Prescriptions.Application.Features.Prescriptions.Create
{
    public class CreatePrescriptionCommandHandler : IRequestHandler<CreatePrescriptionCommand, Result<bool>>
    {
        private readonly IPrescriptionsRepository _prescriptionsRepository;

        public CreatePrescriptionCommandHandler(IPrescriptionsRepository prescriptionsRepository)
            => _prescriptionsRepository = prescriptionsRepository;
        
        public async Task<Result<bool>> Handle(CreatePrescriptionCommand request, CancellationToken cancellationToken)
        {
            var prescription = new Prescription
            {
                AppointmentId = request.AppointmentId,
                PatientId = request.PatientId,
                DoctorId = request.DoctorId,
                Description = request.Description
            };
            var result = await _prescriptionsRepository.Create(prescription, cancellationToken);
            return result 
                ? Result<bool>.Success(true) 
                : Result<bool>.Failure("No se pudo crear la prescripción.");
        }
    }
}
