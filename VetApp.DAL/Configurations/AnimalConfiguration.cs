using VetApp.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace VetApp.DAL.Configurations
{
    public class AnimalConfiguration : IEntityTypeConfiguration<Animal>
    {
        public void Configure(EntityTypeBuilder<Animal> builder)
        {
            builder
                .HasKey(m => m.Id);

            builder
                .Property(m => m.Id)
                .UseIdentityColumn();

            builder
                .Property(m => m.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder
                .Property(m => m.Age)
                .IsRequired()
                .HasMaxLength(50);

            builder
                .Property(m => m.Kind)
                .IsRequired()
                .HasMaxLength(50);

            builder
                .HasOne(m => m.Owner)
                .WithMany(a => a.Animals)
                .HasForeignKey(m => m.OwnerId);

            builder
                .ToTable("Animals");
        }
    }
}
