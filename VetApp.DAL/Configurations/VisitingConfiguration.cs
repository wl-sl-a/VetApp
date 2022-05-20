using VetApp.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace VetApp.DAL.Configurations
{
    public class VisitingConfiguration : IEntityTypeConfiguration<Visiting>
    {
        public void Configure(EntityTypeBuilder<Visiting> builder)
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
                .Property(m => m.Diagnosis)
                .IsRequired()
                .HasMaxLength(150);

            builder
                .Property(m => m.Analyzes)
                .IsRequired()
                .HasMaxLength(150);

            builder
                .Property(m => m.Examination)
                .IsRequired()
                .HasMaxLength(150);

            builder
                .Property(m => m.Medicines)
                .IsRequired()
                .HasMaxLength(150);

            builder
                .HasOne(m => m.Animal)
                .WithMany(a => a.Visitings)
                .HasForeignKey(m => m.AnimalId);

            builder
                .HasOne(m => m.Doctor)
                .WithMany(a => a.Visitings)
                .HasForeignKey(m => m.DoctorId);

            builder
                .ToTable("Visitings");
        }
    }
}
