using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AppAny.Quartz.EntityFrameworkCore.Migrations.SqlServer
{
  public class QuartzFiredTriggerEntityTypeConfiguration : IEntityTypeConfiguration<QuartzFiredTrigger>
  {
    private readonly string? prefix;
    private readonly string? schema;

    public QuartzFiredTriggerEntityTypeConfiguration(string? prefix, string? schema)
    {
      this.prefix = prefix;
      this.schema = schema;
    }

    public void Configure(EntityTypeBuilder<QuartzFiredTrigger> builder)
    {
      builder.ToTable($"{prefix}FIRED_TRIGGERS", schema);

      builder.HasKey(x => new { x.SchedulerName, x.EntryId });

      builder.Property(x => x.SchedulerName)
        .HasColumnName("SCHED_NAME")
        .HasMaxLength(120)
        .IsUnicode()
        .IsRequired();

      builder.Property(x => x.EntryId)
        .HasColumnName("ENTRY_ID")
        .HasMaxLength(140)
        .IsUnicode()
        .IsRequired();

      builder.Property(x => x.TriggerName)
        .HasColumnName("TRIGGER_NAME")
        .HasMaxLength(150)
        .IsUnicode()
        .IsRequired();

      builder.Property(x => x.TriggerGroup)
        .HasColumnName("TRIGGER_GROUP")
        .HasMaxLength(150)
        .IsUnicode()
        .IsRequired();

      builder.Property(x => x.InstanceName)
        .HasColumnName("INSTANCE_NAME")
        .HasMaxLength(150)
        .IsUnicode()
        .IsRequired();

      builder.Property(x => x.FiredTime)
        .HasColumnName("FIRED_TIME")
        .IsRequired();

      builder.Property(x => x.ScheduledTime)
        .HasColumnName("SCHED_TIME")
        .IsRequired();

      builder.Property(x => x.Priority)
        .HasColumnName("PRIORITY")
        .IsRequired();

      builder.Property(x => x.State)
        .HasColumnName("STATE")
        .HasMaxLength(16)
        .IsUnicode()
        .IsRequired();

      builder.Property(x => x.JobName)
        .HasColumnName("JOB_NAME")
        .HasMaxLength(150)
        .IsUnicode();

      builder.Property(x => x.JobGroup)
        .HasColumnName("JOB_GROUP")
        .HasMaxLength(150)
        .IsUnicode();

      builder.Property(x => x.IsNonConcurrent)
        .HasColumnName("IS_NONCONCURRENT");

      builder.Property(x => x.RequestsRecovery)
        .HasColumnName("REQUESTS_RECOVERY");

      builder.HasIndex(x => x.TriggerName)
        .HasDatabaseName($"IDX_{prefix}FT_TRIG_NAME");

      builder.HasIndex(x => x.TriggerGroup)
        .HasDatabaseName($"IDX_{prefix}FT_TRIG_GROUP");

      builder.HasIndex(x => new { x.SchedulerName, x.TriggerName, x.TriggerGroup })
        .HasDatabaseName($"IDX_{prefix}FT_TRIG_NM_GP");

      builder.HasIndex(x => x.InstanceName)
        .HasDatabaseName($"IDX_{prefix}FT_TRIG_INST_NAME");

      builder.HasIndex(x => x.JobName)
        .HasDatabaseName($"IDX_{prefix}FT_JOB_NAME");

      builder.HasIndex(x => x.JobGroup)
        .HasDatabaseName($"IDX_{prefix}FT_JOB_GROUP");

      builder.HasIndex(x => x.RequestsRecovery)
        .HasDatabaseName($"IDX_{prefix}FT_JOB_REQ_RECOVERY");
    }
  }
}
