using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persons.Domain.Entities;

namespace Persons.Infrastructure.Configurations
{
    public sealed class PersonConfiguration : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.Identification).IsRequired().HasMaxLength(20);
            builder.Property(x => x.FirstName).HasMaxLength(100).IsRequired();
            builder.Property(x => x.LastName).HasMaxLength(100).IsRequired();
            builder.Property(x => x.Type).IsRequired();

            builder.HasIndex(x => x.Identification).IsUnique();
         
            builder.Ignore(x => x.FullName);
        }
    }
}
