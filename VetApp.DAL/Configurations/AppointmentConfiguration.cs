using VetApp.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace VetApp.DAL.Configurations
{
    public class AppointmentConfiguration : IEntityTypeConfiguration<Appointment>
    {
        public void Configure(EntityTypeBuilder<Appointment> builder)
        {
            builder
                .HasKey(m => m.Id);

            builder
                .Property(m => m.Id)
                .UseIdentityColumn();

            builder
                .Property(m => m.Date)
                .IsRequired()
                .HasMaxLength(50);

            builder
                .Property(m => m.Time)
                .IsRequired()
                .HasMaxLength(50);

            builder
                .HasOne(m => m.Service)
                .WithMany(a => a.Appointments)
                .HasForeignKey(m => m.ServiceId);

            builder
                .Property(m => m.Status)
                .IsRequired()
                .HasMaxLength(50);

            builder
                .HasOne(m => m.Animal)
                .WithMany(a => a.Appointments)
                .HasForeignKey(m => m.AnimalId);

            builder
                .HasOne(m => m.Doctor)
                .WithMany(a => a.Appointments)
                .HasForeignKey(m => m.DoctorId);

            builder
                .ToTable("Appointments");
        }
    }
}
