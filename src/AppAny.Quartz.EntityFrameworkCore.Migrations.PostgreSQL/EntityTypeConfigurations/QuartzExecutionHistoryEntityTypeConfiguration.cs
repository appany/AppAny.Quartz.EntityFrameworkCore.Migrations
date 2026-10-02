using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AppAny.Quartz.EntityFrameworkCore.Migrations.PostgreSQL
{
  public class QuartzExecutionHistoryEntityTypeConfiguration : IEntityTypeConfiguration<QuartzExecutionHistory>
  {
    private readonly string? prefix;
    private readonly string? schema;

    public QuartzExecutionHistoryEntityTypeConfiguration(string? prefix, string? schema)
    {
      this.prefix = prefix;
      this.schema = schema;
    }

    public void Configure(EntityTypeBuilder<QuartzExecutionHistory> builder)
    {
      builder.ToTable($"{prefix}execution_history", schema);

      builder.HasKey(history => new { history.SchedulerName, history.EntryId });

      builder.Property(history => history.SchedulerName)
        .HasColumnName("sched_name")
        .HasColumnType("text")
        .IsRequired();

      builder.Property(history => history.EntryId)
        .HasColumnName("entry_id")
        .HasColumnType("text")
        .IsRequired();

      builder.Property(history => history.InstanceName)
        .HasColumnName("instance_name")
        .HasColumnType("text")
        .IsRequired();

      builder.Property(history => history.JobName)
        .HasColumnName("job_name")
        .HasColumnType("text")
        .IsRequired();

      builder.Property(history => history.JobGroup)
        .HasColumnName("job_group")
        .HasColumnType("text")
        .IsRequired();

      builder.Property(history => history.TriggerName)
        .HasColumnName("trigger_name")
        .HasColumnType("text")
        .IsRequired();

      builder.Property(history => history.TriggerGroup)
        .HasColumnName("trigger_group")
        .HasColumnType("text")
        .IsRequired();

      builder.Property(history => history.FiredTime)
        .HasColumnName("fired_time")
        .HasColumnType("bigint")
        .IsRequired();

      builder.Property(history => history.RunTime)
        .HasColumnName("run_time")
        .HasColumnType("bigint")
        .IsRequired();

      builder.Property(history => history.Succeeded)
        .HasColumnName("succeeded")
        .HasColumnType("bool")
        .IsRequired();

      builder.Property(history => history.ErrorMessage)
        .HasColumnName("error_message")
        .HasColumnType("text");

      builder.Property(history => history.RetryAttempt)
        .HasColumnName("retry_attempt")
        .HasColumnType("integer")
        .HasDefaultValue(0)
        .IsRequired();

      builder.Property(history => history.RetryScheduled)
        .HasColumnName("retry_scheduled")
        .HasColumnType("bool")
        .HasDefaultValue(false)
        .IsRequired();

      builder.Property(history => history.ExecutionLog)
        .HasColumnName("execution_log")
        .HasColumnType("text");

      builder.HasIndex(history => new { history.SchedulerName, history.FiredTime })
        .HasDatabaseName($"idx_{prefix}eh_fired_time");

      builder.HasIndex(history => new { history.SchedulerName, history.InstanceName })
        .HasDatabaseName($"idx_{prefix}eh_inst");
    }
  }
}