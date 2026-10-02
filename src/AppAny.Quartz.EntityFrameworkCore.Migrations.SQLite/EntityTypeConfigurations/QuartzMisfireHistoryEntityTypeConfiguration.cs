using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AppAny.Quartz.EntityFrameworkCore.Migrations.SQLite;

public class QuartzMisfireHistoryEntityTypeConfiguration : IEntityTypeConfiguration<QuartzMisfireHistory>
{
  private readonly string _prefix;

  public QuartzMisfireHistoryEntityTypeConfiguration(string prefix)
  {
    this._prefix = prefix;
  }

  public void Configure(EntityTypeBuilder<QuartzMisfireHistory> builder)
  {
    builder.ToTable(_prefix + "MISFIRE_HISTORY");

    builder.HasKey(history => new { history.SchedulerName, history.EntryId });

    builder.Property(history => history.SchedulerName)
      .HasColumnName("SCHED_NAME")
      .HasColumnType("text")
      .IsRequired();

    builder.Property(history => history.EntryId)
      .HasColumnName("ENTRY_ID")
      .HasColumnType("text")
      .IsRequired();

    builder.Property(history => history.InstanceName)
      .HasColumnName("INSTANCE_NAME")
      .HasColumnType("text")
      .IsRequired();

    builder.Property(history => history.TriggerName)
      .HasColumnName("TRIGGER_NAME")
      .HasColumnType("text")
      .IsRequired();

    builder.Property(history => history.TriggerGroup)
      .HasColumnName("TRIGGER_GROUP")
      .HasColumnType("text")
      .IsRequired();

    builder.Property(history => history.JobName)
      .HasColumnName("JOB_NAME")
      .HasColumnType("text");

    builder.Property(history => history.JobGroup)
      .HasColumnName("JOB_GROUP")
      .HasColumnType("text");

    builder.Property(history => history.MisfireTime)
      .HasColumnName("MISFIRE_TIME")
      .HasColumnType("bigint")
      .IsRequired();

    builder.Property(history => history.ScheduledTime)
      .HasColumnName("SCHED_TIME")
      .HasColumnType("bigint");

    builder.Property(history => history.Reason)
      .HasColumnName("REASON")
      .HasColumnType("integer");

    builder.HasIndex(history => new { history.SchedulerName, history.MisfireTime })
      .HasDatabaseName($"IDX_{_prefix}MH_MISFIRE_TIME");

    builder.HasIndex(history => new { history.SchedulerName, history.InstanceName })
      .HasDatabaseName($"IDX_{_prefix}MH_INST");
  }
}