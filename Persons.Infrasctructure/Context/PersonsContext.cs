using Microsoft.EntityFrameworkCore;

namespace Persons.Infrastructure
{
    public class PersonsContext: DbContext
    {
        public PersonsContext(DbContextOptions<PersonsContext> options) : base(options){}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(PersonsContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
