using System.Threading;
using System.Threading.Tasks;

namespace Prescriptions.Domain.Services
{
    public interface IHttpService
    {
        Task<T> Get<T>(string url, string endPoint, CancellationToken cancellationToken);
        Task<T> Post<T>(string url, string endPoint, object request, CancellationToken cancellationToken);
    }
}
