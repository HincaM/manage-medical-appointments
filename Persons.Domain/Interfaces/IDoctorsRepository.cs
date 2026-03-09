using Persons.Domain.Entities;
using Persons.Domain.Specifications;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Persons.Domain.Interfaces
{
    public interface IDoctorsRepository
    {
        Task<bool> Delete(int id, CancellationToken cancellationToken);
        Task<Person> Get(DoctorSpecificationBase specification, CancellationToken cancellationToken);
        Task<List<Person>> GetAll(DoctorSpecificationBase specification, CancellationToken cancellationToken);
        Task<bool> Add(Person person, CancellationToken cancellationToken);
        Task<bool> Update(Person person, CancellationToken cancellationToken);
    }
}
