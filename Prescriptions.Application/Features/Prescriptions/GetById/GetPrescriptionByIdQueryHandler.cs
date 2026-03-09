using MediatR;
using Prescriptions.Application.Specifications;
using Prescriptions.Domain.Common;
using Prescriptions.Domain.Entities;
using Prescriptions.Domain.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace Prescriptions.Application.Features.Prescriptions.GetById
{
    public class GetPrescriptionByIdQueryHandler : IRequestHandler<GetPrescriptionByIdQuery, Result<Prescription>>
    {
        private readonly IPrescriptionsRepository _prescriptionsRepository;

        public GetPrescriptionByIdQueryHandler(IPrescriptionsRepository prescriptionsRepository)
            => _prescriptionsRepository = prescriptionsRepository;
        
        public async Task<Result<Prescription>> Handle(GetPrescriptionByIdQuery request, CancellationToken cancellationToken)
        {
            var prescription = await _prescriptionsRepository.Get(new GetByIdSpecification(request.PrescriptionId), cancellationToken);
            return prescription != null 
                ? Result<Prescription>.Success(prescription) 
                : Result<Prescription>.Failure("Receta no encontrada.");
        }
    }
}
