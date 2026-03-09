using System.Threading;
using System.Threading.Tasks;

namespace Prescriptions.Domain.Services
{
    public interface IPatientsService
    {
        Task<int> GetIdByIdentification(string identification, CancellationToken cancellationToken);
    }
}
