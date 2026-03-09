using Microsoft.EntityFrameworkCore;
using Persons.Domain.Entities;
using Persons.Domain.Enums;
using Persons.Domain.Interfaces;
using Persons.Domain.Specifications;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Persons.Infrastructure.Repositories
{
    public class DoctorsRepository : IDoctorsRepository
    {
        private readonly PersonsContext _context;
        public DoctorsRepository(PersonsContext context)
        {
            _context = context;
        }
        public async Task<bool> Add(Person person, CancellationToken cancellationToken)
        {
            person.Type = PersonType.Doctor;
            await _context.Set<Person>().AddAsync(person, cancellationToken);
            return await _context.SaveChangesAsync(cancellationToken) > 0;
        }

        public async Task<bool> Update(Person person, CancellationToken cancellationToken)
        {
            var query = _context.Set<Person>();
            var doctor = await query.FirstOrDefaultAsync(p => p.Id == person.Id && p.Type == PersonType.Doctor, cancellationToken);

            if (doctor != null)
            {
                doctor.FirstName = person.FirstName;
                doctor.LastName = person.LastName;
                doctor.Identification = person.Identification;

                query.Update(doctor);
                return await _context.SaveChangesAsync(cancellationToken) > 0;
            }

            return false;
        }
        
        public async Task<bool> Delete(int id, CancellationToken cancellationToken)
        {
            var query = _context.Set<Person>();
            var doctor = await query.FirstOrDefaultAsync(p => p.Id == id && p.Type == PersonType.Doctor, cancellationToken);
            if(doctor != null)
            {
                query.Remove(doctor);
                return await _context.SaveChangesAsync(cancellationToken) > 0;
            }

            return false;
        }
        
        public async Task<Person> Get(DoctorSpecificationBase specification, CancellationToken cancellationToken)
            => await _context.Set<Person>().FirstOrDefaultAsync(specification.Criteria, cancellationToken);

        public async Task<List<Person>> GetAll(DoctorSpecificationBase specification, CancellationToken cancellationToken)
            => await _context.Set<Person>().Where(specification.Criteria).ToListAsync(cancellationToken);

        
    }
}
