using MediatR;
using Prescriptions.Domain.Common;
using Prescriptions.Domain.Entities;

namespace Prescriptions.Application.Features.Prescriptions.GetById
{
    public class GetPrescriptionByIdQuery: IRequest<Result<Prescription>>
    {
        public int PrescriptionId { get; set; }
    }
}
