using Appointments.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Appointments.Infrastructure.Configurations
{
    public sealed class AppointmentConfiguration : IEntityTypeConfiguration<Appointment>
    {
        public void Configure(EntityTypeBuilder<Appointment> builder)
        {
            builder.HasKey(k => k.Id);
            builder.Property(p => p.Date).IsRequired();
            builder.Property(p => p.Location).HasMaxLength(500);
            builder.Property(p => p.Status).IsRequired();
            builder.Property(p => p.PatientId).IsRequired();
            builder.Property(p => p.DoctorId).IsRequired();
        }
    }
}
