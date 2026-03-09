using MediatR;
using Prescriptions.Domain.Common;
using Prescriptions.Domain.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace Prescriptions.Application.Features.Prescriptions.MarkAsExpired
{
    public class MarkAsExpiredPrescriptionCommandHandler : IRequestHandler<MarkAsExpiredPrescriptionCommand, Result<bool>>
    {
        private readonly IPrescriptionsRepository _prescriptionsRepository;

        public MarkAsExpiredPrescriptionCommandHandler(IPrescriptionsRepository prescriptionsRepository) 
            => _prescriptionsRepository = prescriptionsRepository;

        public async Task<Result<bool>> Handle(MarkAsExpiredPrescriptionCommand request, CancellationToken cancellationToken)
        {
            var result = await _prescriptionsRepository.MarkAsExpired(request.PrescriptionId, cancellationToken);
            return result ? Result<bool>.Success(true) : Result<bool>.Failure("Falló al intentar marcar la receta como vencida.");
        }
    }
}
