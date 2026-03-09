using Microsoft.EntityFrameworkCore;
using Persons.Domain.Entities;
using Persons.Domain.Enums;
using Persons.Domain.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Persons.Infrastructure.Repositories
{
    public class PatientsRepository : IPatientsRepository
    {
        private readonly PersonsContext _context;

        public PatientsRepository(PersonsContext context)
        {
            _context = context;
        }

        public async Task<bool> Add(Person person, CancellationToken cancellationToken)
        {
            person.Type = PersonType.Patient;
            await _context.Set<Person>().AddAsync(person, cancellationToken);
            return await _context.SaveChangesAsync(cancellationToken) > 0;
        }

        public async Task<bool> Update(Person person, CancellationToken cancellationToken)
        {
            var patient = await _context.Set<Person>().Where(p => p.Id == person.Id && p.Type == PersonType.Patient).FirstOrDefaultAsync(cancellationToken);

            if (patient != null)
            {
                patient.FirstName = person.FirstName;
                patient.LastName = person.LastName;
                patient.Identification = person.Identification;
                return await _context.SaveChangesAsync(cancellationToken) > 0;
            }

            return false;

        }


        public async Task<bool> Delete(int id, CancellationToken cancellationToken)
        {
            var query = _context.Set<Person>();

            var patient = await query.FirstOrDefaultAsync(p => p.Id == id && p.Type == PersonType.Patient);
            if (patient != null)
            {
                query.Remove(patient);
                return await _context.SaveChangesAsync(cancellationToken) > 0;
            }

            return false;

        }
        
        public async Task<List<Person>> GetAll(PatientSpecificationBase specification, CancellationToken cancellationToken)
            => await _context.Set<Person>().Where(specification.Criteria).ToListAsync(cancellationToken);


        public async Task<Person> Get(PatientSpecificationBase specification, CancellationToken cancellationToken)
            => await _context.Set<Person>().FirstOrDefaultAsync(specification.Criteria, cancellationToken);

    }
}
