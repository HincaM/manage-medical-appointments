using MediatR;
using Prescriptions.Domain.Common;
using Prescriptions.Domain.Entities;
using System.Collections.Generic;

namespace Prescriptions.Application.Features.Prescriptions.GetByPatientIdentification
{
    public class GetByPatientIdentificationQuery: IRequest<Result<List<Prescription>>>
    {
        public string PatientIdentification { get; set; }
    }
}
