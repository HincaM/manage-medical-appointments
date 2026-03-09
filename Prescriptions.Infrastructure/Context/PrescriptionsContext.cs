using Microsoft.EntityFrameworkCore;

namespace Prescriptions.Infrastructure
{
    public class PrescriptionsContext: DbContext
    {
        public PrescriptionsContext(DbContextOptions<PrescriptionsContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(PrescriptionsContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
