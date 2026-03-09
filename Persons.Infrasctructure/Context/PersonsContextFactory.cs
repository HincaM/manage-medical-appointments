using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Persons.Infrastructure.Context
{
    public class PersonsContextFactory : IDesignTimeDbContextFactory<PersonsContext>
    {
        public PersonsContext CreateDbContext(string[] args)
        {
            //var connectionString = ConfigurationManager
            //    .ConnectionStrings["DefaultConnection"]
            //    .ConnectionString;

            var connectionString =
                "Server=localhost;Database=PersonsDB;" +
                "Integrated Security=True;TrustServerCertificate=True;";

            var optionsBuilder = new DbContextOptionsBuilder<PersonsContext>();
            optionsBuilder.UseSqlServer(connectionString);

            return new PersonsContext(optionsBuilder.Options);
        }
    }
}
