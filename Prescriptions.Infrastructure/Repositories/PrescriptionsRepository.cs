using Microsoft.EntityFrameworkCore;
using Prescriptions.Domain.Entities;
using Prescriptions.Domain.Enums;
using Prescriptions.Domain.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Prescriptions.Infrastructure.Repositories
{
    public class PrescriptionsRepository : IPrescriptionsRepository
    {
        private readonly PrescriptionsContext _context;

        public PrescriptionsRepository(PrescriptionsContext context)
        {
            _context = context;
        }
        public async Task<bool> Create(Prescription prescription, CancellationToken cancellationToken)
        {
            await _context.Set<Prescription>().AddAsync(prescription, cancellationToken);
            return await _context.SaveChangesAsync(cancellationToken) > 0;
        }

        public async Task<bool> Deliver(int prescriptionId, CancellationToken cancellationToken)
        {
            var query = _context.Set<Prescription>();
            var prescription = await query.FirstOrDefaultAsync(p => p.Id == prescriptionId, cancellationToken);
            if (prescription != null) {
                prescription.Status = PrescriptionStatus.Delivered;
                query.Update(prescription);
                return await _context.SaveChangesAsync(cancellationToken) > 0;
            }

            return false;
        }

        public async Task<List<Prescription>> GetAll(Specification<Prescription> specification, CancellationToken cancellationToken) 
            => await _context.Set<Prescription>().Where(specification.Criteria).ToListAsync(cancellationToken);

        public async Task<Prescription> Get(Specification<Prescription> specification, CancellationToken cancellationToken) 
            => await _context.Set<Prescription>().FirstOrDefaultAsync(specification.Criteria, cancellationToken);

        public async Task<bool> MarkAsExpired(int prescriptionId, CancellationToken cancellationToken)
        {
            var query = _context.Set<Prescription>();
            var prescription = await query.FirstOrDefaultAsync(p => p.Id == prescriptionId, cancellationToken);
            if (prescription != null)
            {
                prescription.Status = PrescriptionStatus.Expired;
                query.Update(prescription);
                return await _context.SaveChangesAsync(cancellationToken) > 0;
            }

            return false;
        }

        public async Task<bool> Delete(int prescriptionId, CancellationToken cancellationToken)
        {
            var query = _context.Set<Prescription>();
            var prescription = await query.FirstOrDefaultAsync(p => p.Id == prescriptionId, cancellationToken);
            if (prescription != null)
            {
                query.Remove(prescription);
                return await _context.SaveChangesAsync(cancellationToken) > 0;
            }

            return false;
        }


    }
}
