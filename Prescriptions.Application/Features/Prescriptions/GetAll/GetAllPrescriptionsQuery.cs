using MediatR;
using Prescriptions.Domain.Common;
using Prescriptions.Domain.Entities;
using System.Collections.Generic;

namespace Prescriptions.Application.Features.Prescriptions.GetAll
{
    public class GetAllPrescriptionsQuery : IRequest<Result<List<Prescription>>>
    {
        public List<int> Ids { get; set; }
    }
}
