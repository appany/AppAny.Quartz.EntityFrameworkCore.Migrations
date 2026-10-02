using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AppAny.Quartz.EntityFrameworkCore.Migrations.SQLite;

public class QuartzFiredTriggerEntityTypeConfiguration : IEntityTypeConfiguration<QuartzFiredTrigger>
{
  private readonly string _prefix;

  public QuartzFiredTriggerEntityTypeConfiguration(string prefix)
  {
    this._prefix = prefix;
  }

  public void Configure(EntityTypeBuilder<QuartzFiredTrigger> builder)
  {
    builder.ToTable(_prefix + "FIRED_TRIGGERS");

    builder.HasKey(x => new { x.SchedulerName, x.EntryId });

    builder.Property(x => x.SchedulerName)
      .HasColumnName("SCHED_NAME")
      .HasColumnType("text")
      .IsRequired();

    builder.Property(x => x.EntryId)
      .HasColumnName("ENTRY_ID")
      .HasColumnType("text")
      .IsRequired();

    builder.Property(x => x.TriggerName)
      .HasColumnName("TRIGGER_NAME")
      .HasColumnType("text")
      .IsRequired();

    builder.Property(x => x.TriggerGroup)
      .HasColumnName("TRIGGER_GROUP")
      .HasColumnType("text")
      .IsRequired();

    builder.Property(x => x.InstanceName)
      .HasColumnName("INSTANCE_NAME")
      .HasColumnType("text")
      .IsRequired();

    builder.Property(x => x.FiredTime)
      .HasColumnName("FIRED_TIME")
      .HasColumnType("bigint")
      .IsRequired();

    builder.Property(x => x.ScheduledTime)
      .HasColumnName("SCHED_TIME")
      .HasColumnType("bigint")
      .IsRequired();

    builder.Property(x => x.Priority)
      .HasColumnName("PRIORITY")
      .HasColumnType("integer")
      .IsRequired();

    builder.Property(x => x.State)
      .HasColumnName("STATE")
      .HasColumnType("text")
      .IsRequired();

    builder.Property(x => x.JobName)
      .HasColumnName("JOB_NAME")
      .HasColumnType("text");

    builder.Property(x => x.JobGroup)
      .HasColumnName("JOB_GROUP")
      .HasColumnType("text");

    builder.Property(x => x.IsNonConcurrent)
      .HasColumnName("IS_NONCONCURRENT")
      .HasColumnType("bool")
      .IsRequired();

    builder.Property(x => x.RequestsRecovery)
      .HasColumnName("REQUESTS_RECOVERY")
      .HasColumnType("bool");

    builder.Property(x => x.ExecutionGroup)
      .HasColumnName("EXECUTION_GROUP")
      .HasColumnType("text");

    builder.Property(trigger => trigger.Progress)
      .HasColumnName("PROGRESS")
      .HasColumnType("integer");

    builder.Property(trigger => trigger.ProgressMessage)
      .HasColumnName("PROGRESS_MESSAGE")
      .HasColumnType("text");

    builder.HasIndex(x => new { x.SchedulerName, x.InstanceName, x.RequestsRecovery })
      .HasDatabaseName($"IDX_{_prefix}FT_INST_JOB_REQ_RCVRY");

    builder.HasIndex(x => new { x.SchedulerName, x.JobName, x.JobGroup })
      .HasDatabaseName($"IDX_{_prefix}FT_J_G");

    builder.HasIndex(x => new { x.SchedulerName, x.TriggerName, x.TriggerGroup })
      .HasDatabaseName($"IDX_{_prefix}FT_T_G");
  }
}
