using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Prescriptions.Domain.Entities;

namespace Prescriptions.Infrastructure.Configurations
{
    public sealed class PrescriptionConfiguration : IEntityTypeConfiguration<Prescription>
    {
        public void Configure(EntityTypeBuilder<Prescription> builder)
        {
            builder.HasKey(k => k.Id);
            builder.Property(p => p.Id).ValueGeneratedOnAdd();
            builder.Property(p => p.AppointmentId).IsRequired();
            builder.Property(p => p.PatientId).IsRequired();
            builder.Property(p => p.DoctorId).IsRequired();
            builder.Property(p => p.Description).IsRequired().HasMaxLength(1000);
            builder.Property(p => p.Status).IsRequired();
            builder.Property(p => p.CreatedAt).IsRequired().HasDefaultValueSql("GETDATE()");
        }
    }
}
