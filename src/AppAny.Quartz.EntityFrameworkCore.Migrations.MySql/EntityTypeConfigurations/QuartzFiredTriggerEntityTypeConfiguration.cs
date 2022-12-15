using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AppAny.Quartz.EntityFrameworkCore.Migrations.MySql
{
  public class QuartzFiredTriggerEntityTypeConfiguration : IEntityTypeConfiguration<QuartzFiredTrigger>
  {
    private readonly string? prefix;

    public QuartzFiredTriggerEntityTypeConfiguration(string? prefix)
    {
      this.prefix = prefix;
    }

    public void Configure(EntityTypeBuilder<QuartzFiredTrigger> builder)
    {
      builder.ToTable($"{prefix}fired_triggers");

      builder.HasKey(x => new { x.SchedulerName, x.EntryId });

      builder.Property(x => x.SchedulerName)
        .HasColumnName("sched_name")
        .HasColumnType("varchar(120)")
        .IsRequired();

      builder.Property(x => x.EntryId)
        .HasColumnName("entry_id")
        .HasColumnType("varchar(140)")
        .IsRequired();

      builder.Property(x => x.TriggerName)
        .HasColumnName("trigger_name")
        .HasColumnType("varchar(200)")
        .IsRequired();

      builder.Property(x => x.TriggerGroup)
        .HasColumnName("trigger_group")
        .HasColumnType("varchar(200)")
        .IsRequired();

      builder.Property(x => x.InstanceName)
        .HasColumnName("instance_name")
        .HasColumnType("varchar(200)")
        .IsRequired();

      builder.Property(x => x.FiredTime)
        .HasColumnName("fired_time")
        .HasColumnType("bigint(19)")
        .IsRequired();

      builder.Property(x => x.ScheduledTime)
        .HasColumnName("sched_time")
        .HasColumnType("bigint(19)")
        .IsRequired();

      builder.Property(x => x.Priority)
        .HasColumnName("priority")
        .HasColumnType("integer")
        .IsRequired();

      builder.Property(x => x.State)
        .HasColumnName("state")
        .HasColumnType("varchar(16)")
        .IsRequired();

      builder.Property(x => x.JobName)
        .HasColumnName("job_name")
        .HasColumnType("varchar(200)");

      builder.Property(x => x.JobGroup)
        .HasColumnName("job_group")
        .HasColumnType("varchar(200)");

      builder.Property(x => x.IsNonConcurrent)
        .HasColumnName("is_nonconcurrent")
        .HasColumnType("tinyint(1)")
        .IsRequired();

      builder.Property(x => x.RequestsRecovery)
        .HasColumnName("requests_recovery")
        .HasColumnType("tinyint(1)");

      builder.HasIndex(x => x.TriggerName)
        .HasDatabaseName($"idx_{prefix}ft_trig_name");

      builder.HasIndex(x => x.TriggerGroup)
        .HasDatabaseName($"idx_{prefix}ft_trig_group");

      builder.HasIndex(x => new { x.SchedulerName, x.TriggerName, x.TriggerGroup })
        .HasDatabaseName($"idx_{prefix}ft_trig_nm_gp");

      builder.HasIndex(x => x.InstanceName)
        .HasDatabaseName($"idx_{prefix}ft_trig_inst_name");

      builder.HasIndex(x => x.JobName)
        .HasDatabaseName($"idx_{prefix}ft_job_name");

      builder.HasIndex(x => x.JobGroup)
        .HasDatabaseName($"idx_{prefix}ft_job_group");

      builder.HasIndex(x => x.RequestsRecovery)
        .HasDatabaseName($"idx_{prefix}ft_job_req_recovery");
    }
  }
}
