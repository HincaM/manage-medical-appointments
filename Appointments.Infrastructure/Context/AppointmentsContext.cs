using Microsoft.EntityFrameworkCore;

namespace Appointments.Infrastructure
{
    public class AppointmentsContext : DbContext
    {       
        public AppointmentsContext(DbContextOptions<AppointmentsContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppointmentsContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
