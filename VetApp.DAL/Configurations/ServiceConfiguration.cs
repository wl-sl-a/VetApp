using VetApp.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace VetApp.DAL.Configurations
{
    public class ServiceConfiguration : IEntityTypeConfiguration<Service>
    {
        public void Configure(EntityTypeBuilder<Service> builder)
        {
            builder
                .HasKey(a => a.Id);

            builder
                .Property(m => m.Id)
                .UseIdentityColumn();

            builder
                .Property(m => m.Name)
                .IsRequired()
                .HasMaxLength(50);
            builder
                .Property(m => m.Price)
                .IsRequired()
                .HasMaxLength(50);
            builder
                .Property(m => m.Info)
                .IsRequired()
                .HasMaxLength(50);

            builder
                .Property(m => m.VetName)
                .IsRequired()
                .HasMaxLength(50);

            builder
                .ToTable("Services");
        }
    }
}
