using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AppAny.Quartz.EntityFrameworkCore.Migrations.SqlServer
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
      builder.ToTable($"{prefix}EXECUTION_HISTORY", schema);

      builder.HasKey(history => new { history.SchedulerName, history.EntryId });

      builder.Property(history => history.SchedulerName)
        .HasColumnName("SCHED_NAME")
        .HasMaxLength(120)
        .IsUnicode()
        .IsRequired();

      builder.Property(history => history.EntryId)
        .HasColumnName("ENTRY_ID")
        .HasMaxLength(140)
        .IsUnicode()
        .IsRequired();

      builder.Property(history => history.InstanceName)
        .HasColumnName("INSTANCE_NAME")
        .HasMaxLength(200)
        .IsUnicode()
        .IsRequired();

      builder.Property(history => history.JobName)
        .HasColumnName("JOB_NAME")
        .HasMaxLength(150)
        .IsUnicode()
        .IsRequired();

      builder.Property(history => history.JobGroup)
        .HasColumnName("JOB_GROUP")
        .HasMaxLength(150)
        .IsUnicode()
        .IsRequired();

      builder.Property(history => history.TriggerName)
        .HasColumnName("TRIGGER_NAME")
        .HasMaxLength(150)
        .IsUnicode()
        .IsRequired();

      builder.Property(history => history.TriggerGroup)
        .HasColumnName("TRIGGER_GROUP")
        .HasMaxLength(150)
        .IsUnicode()
        .IsRequired();

      builder.Property(history => history.FiredTime)
        .HasColumnName("FIRED_TIME")
        .IsRequired();

      builder.Property(history => history.RunTime)
        .HasColumnName("RUN_TIME")
        .IsRequired();

      builder.Property(history => history.Succeeded)
        .HasColumnName("SUCCEEDED")
        .IsRequired();

      builder.Property(history => history.ErrorMessage)
        .HasColumnName("ERROR_MESSAGE")
        .HasMaxLength(1000)
        .IsUnicode();

      builder.Property(history => history.RetryAttempt)
        .HasColumnName("RETRY_ATTEMPT")
        .HasDefaultValue(0)
        .IsRequired();

      builder.Property(history => history.RetryScheduled)
        .HasColumnName("RETRY_SCHEDULED")
        .HasDefaultValue(false)
        .IsRequired();

      builder.Property(history => history.ExecutionLog)
        .HasColumnName("EXECUTION_LOG")
        .IsUnicode();

      builder.HasIndex(history => new { history.SchedulerName, history.FiredTime })
        .HasDatabaseName($"IDX_{prefix}EH_FIRED_TIME");

      builder.HasIndex(history => new { history.SchedulerName, history.InstanceName })
        .HasDatabaseName($"IDX_{prefix}EH_INST");
    }
  }
}