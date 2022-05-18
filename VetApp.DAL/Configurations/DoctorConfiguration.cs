using VetApp.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace VetApp.DAL.Configurations
{
    public class DoctorConfiguration : IEntityTypeConfiguration<Doctor>
    {
        public void Configure(EntityTypeBuilder<Doctor> builder)
        {
            builder
                .HasKey(a => a.Id);

            builder
                .Property(m => m.Id)
                .UseIdentityColumn();

            builder
                .Property(m => m.Surname)
                .IsRequired()
                .HasMaxLength(50);

            builder
                .Property(m => m.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder
                .Property(m => m.Phone)
                .IsRequired()
                .HasMaxLength(50);

            builder
                .Property(m => m.Specialty)
                .IsRequired()
                .HasMaxLength(50);

            builder
                .Property(m => m.VetName)
                .IsRequired()
                .HasMaxLength(50);

            builder
                .ToTable("Doctors");
        }
    }
}
