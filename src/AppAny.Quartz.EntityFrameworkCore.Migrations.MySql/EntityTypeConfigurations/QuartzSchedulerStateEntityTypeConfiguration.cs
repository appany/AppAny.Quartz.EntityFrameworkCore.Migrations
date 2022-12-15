using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AppAny.Quartz.EntityFrameworkCore.Migrations.MySql
{
  public class QuartzSchedulerStateEntityTypeConfiguration : IEntityTypeConfiguration<QuartzSchedulerState>
  {
    private readonly string? prefix;

    public QuartzSchedulerStateEntityTypeConfiguration(string? prefix)
    {
      this.prefix = prefix;
    }

    public void Configure(EntityTypeBuilder<QuartzSchedulerState> builder)
    {
      builder.ToTable($"{prefix}scheduler_state");

      builder.HasKey(x => new { x.SchedulerName, x.InstanceName });

      builder.Property(x => x.SchedulerName)
        .HasColumnName("sched_name")
        .HasColumnType("varchar(120)")
        .IsRequired();

      builder.Property(x => x.InstanceName)
        .HasColumnName("instance_name")
        .HasColumnType("varchar(200)")
        .IsRequired();

      builder.Property(x => x.LastCheckInTime)
        .HasColumnName("last_checkin_time")
        .HasColumnType("bigint(19)")
        .IsRequired();

      builder.Property(x => x.CheckInInterval)
        .HasColumnName("checkin_interval")
        .HasColumnType("bigint(19)")
        .IsRequired();
    }
  }
}
