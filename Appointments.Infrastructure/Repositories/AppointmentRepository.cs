using Appointments.Domain.Entities;
using Appointments.Domain.Enums;
using Appointments.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Appointments.Infrastructure.Repositories
{
    public sealed class AppointmentRepository : IAppointmentRepository
    {
        private readonly AppointmentsContext _context;
        public AppointmentRepository(AppointmentsContext context)
        {
            _context = context;
        }
        public async Task<bool> Schedule(Appointment appointment, CancellationToken cancellationToken)
        {
            await _context.Set<Appointment>().AddAsync(appointment, cancellationToken);
            return await _context.SaveChangesAsync(cancellationToken) > 0;
        }
        public async Task<bool> Start(int appointmentId, CancellationToken cancellationToken)
        {
            var query = _context.Set<Appointment>();
            var appointment = await query.FirstOrDefaultAsync(w => w.Id == appointmentId, cancellationToken);

            if(appointment != null)
            {
                appointment.Status = AppointmentStatus.InProgress;
                query.Update(appointment);
                return await _context.SaveChangesAsync(cancellationToken) > 0;
            }

            return false;
        }
        public async Task<bool> Finish(int appointmentId, CancellationToken cancellationToken)
        {

            var query = _context.Set<Appointment>();
            var appointment = await query.FirstOrDefaultAsync(w => w.Id == appointmentId, cancellationToken);

            if (appointment != null)
            {
                appointment.Status = AppointmentStatus.Completed;
                query.Update(appointment);
                return await _context.SaveChangesAsync(cancellationToken) > 0;
            }

            return false;
        }
    }
}
