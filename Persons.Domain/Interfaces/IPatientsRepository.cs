using Persons.Domain.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Persons.Domain.Interfaces
{
    public interface IPatientsRepository
    {
        Task<Person> Get(PatientSpecificationBase specification, CancellationToken cancellationToken);
        Task<List<Person>> GetAll(PatientSpecificationBase specification, CancellationToken cancellationToken);
        Task<bool> Add(Person person, CancellationToken cancellationToken);
        Task<bool> Update(Person person, CancellationToken cancellationToken);
        Task<bool> Delete(int id, CancellationToken cancellationToken);
    }
}
