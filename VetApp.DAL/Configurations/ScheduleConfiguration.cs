using VetApp.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace VetApp.DAL.Configurations
{
    public class ScheduleConfiguration : IEntityTypeConfiguration<Schedule>
    {
        public void Configure(EntityTypeBuilder<Schedule> builder)
        {
            builder
                .HasKey(m => m.Id);

            builder
                .Property(m => m.Id)
                .UseIdentityColumn();

            builder
                .Property(m => m.Weekday)
                .IsRequired()
                .HasMaxLength(50);

            builder
                .Property(m => m.StartTime)
                .IsRequired()
                .HasMaxLength(50);

            builder
                .Property(m => m.EndTime)
                .IsRequired()
                .HasMaxLength(50);

            builder
                .HasOne(m => m.Doctor)
                .WithMany(a => a.Schedules)
                .HasForeignKey(m => m.DoctorId);

            builder
                .ToTable("Schedules");
        }
    }
}
