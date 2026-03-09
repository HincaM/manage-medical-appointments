using MediatR;
using Prescriptions.Domain.Common;
using Prescriptions.Domain.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace Prescriptions.Application.Features.Prescriptions.Deliver
{
    public class DeliverPrescriptionCommandHandler : IRequestHandler<DeliverPrescriptionCommand, Result<bool>>
    {
        private readonly IPrescriptionsRepository _prescriptionsRepository;

        public DeliverPrescriptionCommandHandler(IPrescriptionsRepository prescriptionsRepository)
        {
            _prescriptionsRepository = prescriptionsRepository;
        }

        public async Task<Result<bool>> Handle(DeliverPrescriptionCommand request, CancellationToken cancellationToken)
        {
            var result = await _prescriptionsRepository.Deliver(request.PrescriptionId, cancellationToken);
            return result ? Result<bool>.Success(true) : Result<bool>.Failure("Falló al intentar realizar entrega de receta.");
        }
    }
}
