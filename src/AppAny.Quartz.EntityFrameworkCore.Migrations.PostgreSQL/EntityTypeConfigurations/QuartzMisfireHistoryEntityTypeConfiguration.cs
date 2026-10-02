using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AppAny.Quartz.EntityFrameworkCore.Migrations.PostgreSQL
{
  public class QuartzMisfireHistoryEntityTypeConfiguration : IEntityTypeConfiguration<QuartzMisfireHistory>
  {
    private readonly string? prefix;
    private readonly string? schema;

    public QuartzMisfireHistoryEntityTypeConfiguration(string? prefix, string? schema)
    {
      this.prefix = prefix;
      this.schema = schema;
    }

    public void Configure(EntityTypeBuilder<QuartzMisfireHistory> builder)
    {
      builder.ToTable($"{prefix}misfire_history", schema);

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

      builder.Property(history => history.TriggerName)
        .HasColumnName("trigger_name")
        .HasColumnType("text")
        .IsRequired();

      builder.Property(history => history.TriggerGroup)
        .HasColumnName("trigger_group")
        .HasColumnType("text")
        .IsRequired();

      builder.Property(history => history.JobName)
        .HasColumnName("job_name")
        .HasColumnType("text");

      builder.Property(history => history.JobGroup)
        .HasColumnName("job_group")
        .HasColumnType("text");

      builder.Property(history => history.MisfireTime)
        .HasColumnName("misfire_time")
        .HasColumnType("bigint")
        .IsRequired();

      builder.Property(history => history.ScheduledTime)
        .HasColumnName("sched_time")
        .HasColumnType("bigint");

      builder.Property(history => history.Reason)
        .HasColumnName("reason")
        .HasColumnType("integer");

      builder.HasIndex(history => new { history.SchedulerName, history.MisfireTime })
        .HasDatabaseName($"idx_{prefix}mh_misfire_time");

      builder.HasIndex(history => new { history.SchedulerName, history.InstanceName })
        .HasDatabaseName($"idx_{prefix}mh_inst");
    }
  }
}