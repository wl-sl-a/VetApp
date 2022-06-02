using VetApp.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace VetApp.DAL.Configurations
{
    public class DirectionConfiguration : IEntityTypeConfiguration<Direction>
    {
        public void Configure(EntityTypeBuilder<Direction> builder)
        {
            builder
                .HasKey(m => m.Id);

            builder
                .Property(m => m.Id)
                .UseIdentityColumn();

            builder
                .HasOne(m => m.Service)
                .WithMany(a => a.Directions)
                .HasForeignKey(m => m.ServiceId);

            builder
                .HasOne(m => m.Visiting)
                .WithMany(a => a.Directions)
                .HasForeignKey(m => m.VisitingId);

            builder
                .ToTable("Directions");
        }
    }
}
