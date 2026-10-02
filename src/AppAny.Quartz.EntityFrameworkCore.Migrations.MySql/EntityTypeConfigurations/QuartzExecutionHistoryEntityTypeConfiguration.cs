using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AppAny.Quartz.EntityFrameworkCore.Migrations.MySql
{
  public class QuartzExecutionHistoryEntityTypeConfiguration : IEntityTypeConfiguration<QuartzExecutionHistory>
  {
    private readonly string? prefix;

    public QuartzExecutionHistoryEntityTypeConfiguration(string? prefix)
    {
      this.prefix = prefix;
    }

    public void Configure(EntityTypeBuilder<QuartzExecutionHistory> builder)
    {
      builder.ToTable($"{prefix}EXECUTION_HISTORY");

      builder.HasKey(history => new { history.SchedulerName, history.EntryId });

      builder.Property(history => history.SchedulerName)
        .HasColumnName("SCHED_NAME")
        .HasColumnType("varchar(120)")
        .IsRequired();

      builder.Property(history => history.EntryId)
        .HasColumnName("ENTRY_ID")
        .HasColumnType("varchar(140)")
        .IsRequired();

      builder.Property(history => history.InstanceName)
        .HasColumnName("INSTANCE_NAME")
        .HasColumnType("varchar(200)")
        .IsRequired();

      builder.Property(history => history.JobName)
        .HasColumnName("JOB_NAME")
        .HasColumnType("varchar(200)")
        .IsRequired();

      builder.Property(history => history.JobGroup)
        .HasColumnName("JOB_GROUP")
        .HasColumnType("varchar(200)")
        .IsRequired();

      builder.Property(history => history.TriggerName)
        .HasColumnName("TRIGGER_NAME")
        .HasColumnType("varchar(200)")
        .IsRequired();

      builder.Property(history => history.TriggerGroup)
        .HasColumnName("TRIGGER_GROUP")
        .HasColumnType("varchar(200)")
        .IsRequired();

      builder.Property(history => history.FiredTime)
        .HasColumnName("FIRED_TIME")
        .HasColumnType("bigint(19)")
        .IsRequired();

      builder.Property(history => history.RunTime)
        .HasColumnName("RUN_TIME")
        .HasColumnType("bigint(19)")
        .IsRequired();

      builder.Property(history => history.Succeeded)
        .HasColumnName("SUCCEEDED")
        .HasColumnType("tinyint(1)")
        .IsRequired();

      builder.Property(history => history.ErrorMessage)
        .HasColumnName("ERROR_MESSAGE")
        .HasColumnType("varchar(1000)");

      builder.Property(history => history.RetryAttempt)
        .HasColumnName("RETRY_ATTEMPT")
        .HasColumnType("integer")
        .HasDefaultValue(0)
        .IsRequired();

      builder.Property(history => history.RetryScheduled)
        .HasColumnName("RETRY_SCHEDULED")
        .HasColumnType("tinyint(1)")
        .HasDefaultValue(false)
        .IsRequired();

      builder.Property(history => history.ExecutionLog)
        .HasColumnName("EXECUTION_LOG")
        .HasColumnType("longtext");

      builder.HasIndex(history => new { history.SchedulerName, history.FiredTime })
        .HasDatabaseName($"IDX_{prefix}EH_FIRED_TIME");

      builder.HasIndex(history => new { history.SchedulerName, history.InstanceName })
        .HasDatabaseName($"IDX_{prefix}EH_INST");
    }
  }
}