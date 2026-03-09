using MediatR;
using Prescriptions.Domain.Common;

namespace Prescriptions.Application.Features.Prescriptions.MarkAsExpired
{
    public class MarkAsExpiredPrescriptionCommand: IRequest<Result<bool>>
    {
        public int PrescriptionId { get; set; }
    }
}
