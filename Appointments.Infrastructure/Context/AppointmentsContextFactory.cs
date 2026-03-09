using Appointments.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Appointments.Infrastructure.Context
{
    public class AppointmentsContextFactory : IDesignTimeDbContextFactory<AppointmentsContext>
    {
        public AppointmentsContext CreateDbContext(string[] args)
        {
            var connectionString =
                "Server=localhost;Database=AppointmentsDB;" +
                "Integrated Security=True;TrustServerCertificate=True;";

            var optionsBuilder = new DbContextOptionsBuilder<AppointmentsContext>();
            optionsBuilder.UseSqlServer(connectionString);

            return new AppointmentsContext(optionsBuilder.Options);
        }
    }
}
