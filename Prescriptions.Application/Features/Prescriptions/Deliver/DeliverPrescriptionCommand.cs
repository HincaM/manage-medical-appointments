using MediatR;
using Prescriptions.Domain.Common;

namespace Prescriptions.Application.Features.Prescriptions.Deliver
{
    public class DeliverPrescriptionCommand: IRequest<Result<bool>>
    {
        public int PrescriptionId { get; set; }
    }
}
