using MediatR;
using Prescriptions.Application.Specifications;
using Prescriptions.Domain.Common;
using Prescriptions.Domain.Entities;
using Prescriptions.Domain.Interfaces;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Prescriptions.Application.Features.Prescriptions.GetAll
{
    public class GetAllPrescriptionsQueryHandler : IRequestHandler<GetAllPrescriptionsQuery, Result<List<Prescription>>>
    {
        private readonly IPrescriptionsRepository _prescriptionsRepository;

        public GetAllPrescriptionsQueryHandler(IPrescriptionsRepository prescriptionRepository)
            => _prescriptionsRepository = prescriptionRepository;
        
        public async Task<Result<List<Prescription>>> Handle(GetAllPrescriptionsQuery request, CancellationToken cancellationToken)
        {
            var result = await _prescriptionsRepository.GetAll(new GetPrescriptionsSpecification(request.Ids), cancellationToken);
            return result != null 
                ? Result<List<Prescription>>.Success(result) 
                : Result<List<Prescription>>.Failure("No se encontraron prescripciones.");
        }
    }
}
