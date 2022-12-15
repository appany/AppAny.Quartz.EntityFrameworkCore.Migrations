using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AppAny.Quartz.EntityFrameworkCore.Migrations.SQLite.EntityTypeConfigurations;

public class QuartzSchedulerStateEntityTypeConfiguration : IEntityTypeConfiguration<QuartzSchedulerState>
{
  private readonly string _prefix;
  private readonly string _schema;

  public QuartzSchedulerStateEntityTypeConfiguration(string prefix, string schema)
  {
    this._prefix = prefix;
    this._schema = schema;
  }

  public void Configure(EntityTypeBuilder<QuartzSchedulerState> builder)
  {
    builder.ToTable(_prefix + "SCHEDULER_STATE", _schema);

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

