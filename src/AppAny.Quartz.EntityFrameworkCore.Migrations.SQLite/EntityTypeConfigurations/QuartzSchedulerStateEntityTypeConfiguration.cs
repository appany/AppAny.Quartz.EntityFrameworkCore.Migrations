using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AppAny.Quartz.EntityFrameworkCore.Migrations.SQLite.EntityTypeConfigurations;

public class QuartzSchedulerStateEntityTypeConfiguration : IEntityTypeConfiguration<QuartzSchedulerState>
{
  private readonly string? prefix;
  private readonly string? schema;

  public QuartzSchedulerStateEntityTypeConfiguration(string? prefix, string? schema)
  {
    this.prefix = prefix;
    this.schema = schema;
  }

  public void Configure(EntityTypeBuilder<QuartzSchedulerState> builder)
  {
    builder.ToTable(prefix + "SCHEDULER_STATE", schema);

    builder.HasKey(x => new { x.SchedulerName, x.InstanceName });

    builder.Property(x => x.SchedulerName)
      .HasColumnName("SCHED_NAME")
      .HasColumnType("text")
      .IsRequired();

    builder.Property(x => x.InstanceName)
      .HasColumnName("INSTANCE_NAME")
      .HasColumnType("text")
      .IsRequired();

    builder.Property(x => x.LastCheckInTime)
      .HasColumnName("LAST_CHECKIN_TIME")
      .HasColumnType("bigint")
      .IsRequired();

    builder.Property(x => x.CheckInInterval)
      .HasColumnName("CHECKIN_INTERVAL")
      .HasColumnType("bigint")
      .IsRequired();
  }
}

