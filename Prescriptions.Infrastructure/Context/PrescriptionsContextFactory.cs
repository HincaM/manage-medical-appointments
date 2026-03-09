using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Prescriptions.Domain.Interfaces;

namespace Prescriptions.Infrastructure.Context
{
    public class PrescriptionsContextFactory : IDesignTimeDbContextFactory<PrescriptionsContext>
    {
        private readonly IConnectionString _connectionString;
        public PrescriptionsContextFactory(IConnectionString connectionString) 
            => _connectionString = connectionString;
        public PrescriptionsContext CreateDbContext(string[] args)
        {
            //var connectionString = ConfigurationManager
            //    .ConnectionStrings["DefaultConnection"]
            //    .ConnectionString;

            //var connectionString =
            //    "Server=localhost;Database=PrescriptionsDB;" +
            //    "Integrated Security=True;TrustServerCertificate=True;";

            var optionsBuilder = new DbContextOptionsBuilder<PrescriptionsContext>();
            optionsBuilder.UseSqlServer(_connectionString.Value);

            return new PrescriptionsContext(optionsBuilder.Options);
        }
    }
}
