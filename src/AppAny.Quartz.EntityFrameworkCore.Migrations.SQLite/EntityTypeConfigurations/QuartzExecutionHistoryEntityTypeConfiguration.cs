using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AppAny.Quartz.EntityFrameworkCore.Migrations.SQLite;

public class QuartzExecutionHistoryEntityTypeConfiguration : IEntityTypeConfiguration<QuartzExecutionHistory>
{
  private readonly string _prefix;

  public QuartzExecutionHistoryEntityTypeConfiguration(string prefix)
  {
    this._prefix = prefix;
  }

  public void Configure(EntityTypeBuilder<QuartzExecutionHistory> builder)
  {
    builder.ToTable(_prefix + "EXECUTION_HISTORY");

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

    builder.Property(history => history.JobName)
      .HasColumnName("JOB_NAME")
      .HasColumnType("text")
      .IsRequired();

    builder.Property(history => history.JobGroup)
      .HasColumnName("JOB_GROUP")
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

    builder.Property(history => history.FiredTime)
      .HasColumnName("FIRED_TIME")
      .HasColumnType("bigint")
      .IsRequired();

    builder.Property(history => history.RunTime)
      .HasColumnName("RUN_TIME")
      .HasColumnType("bigint")
      .IsRequired();

    builder.Property(history => history.Succeeded)
      .HasColumnName("SUCCEEDED")
      .HasColumnType("bit")
      .IsRequired();

    builder.Property(history => history.ErrorMessage)
      .HasColumnName("ERROR_MESSAGE")
      .HasColumnType("text");

    builder.Property(history => history.RetryAttempt)
      .HasColumnName("RETRY_ATTEMPT")
      .HasColumnType("integer")
      .HasDefaultValue(0)
      .IsRequired();

    builder.Property(history => history.RetryScheduled)
      .HasColumnName("RETRY_SCHEDULED")
      .HasColumnType("bit")
      .HasDefaultValue(false)
      .IsRequired();

    builder.Property(history => history.ExecutionLog)
      .HasColumnName("EXECUTION_LOG")
      .HasColumnType("text");

    builder.HasIndex(history => new { history.SchedulerName, history.FiredTime })
      .HasDatabaseName($"IDX_{_prefix}EH_FIRED_TIME");

    builder.HasIndex(history => new { history.SchedulerName, history.InstanceName })
      .HasDatabaseName($"IDX_{_prefix}EH_INST");
  }
}