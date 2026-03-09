using Appointments.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace Appointments.Domain.Interfaces
{
    public interface IAppointmentRepository
    {
        Task<bool> Schedule(Appointment appointment, CancellationToken cancellationToken);
        Task<bool> Start(int appointmentId, CancellationToken cancellationToken);
        Task<bool> Finish(int appointmentId, CancellationToken cancellationToken);
    }
}
