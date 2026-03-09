using Prescriptions.Domain.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Prescriptions.Domain.Interfaces
{
    public interface IPrescriptionsRepository
    {
        Task<bool> Deliver(int prescriptionId, CancellationToken cancellationToken);
        Task<bool> MarkAsExpired(int prescriptionId, CancellationToken cancellationToken);
        Task<Prescription> Get(Specification<Prescription> specification, CancellationToken cancellationToken);
        Task<List<Prescription>> GetAll(Specification<Prescription> specification, CancellationToken cancellationToken);
        Task<bool> Create(Prescription prescription, CancellationToken cancellationToken);
        Task<bool> Delete(int prescriptionId, CancellationToken cancellationToken);
    }
}
