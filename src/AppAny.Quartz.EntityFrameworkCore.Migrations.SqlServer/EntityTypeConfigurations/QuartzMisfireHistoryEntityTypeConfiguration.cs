using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AppAny.Quartz.EntityFrameworkCore.Migrations.SqlServer
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
      builder.ToTable($"{prefix}MISFIRE_HISTORY", schema);

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

      builder.Property(history => history.JobName)
        .HasColumnName("JOB_NAME")
        .HasMaxLength(150)
        .IsUnicode();

      builder.Property(history => history.JobGroup)
        .HasColumnName("JOB_GROUP")
        .HasMaxLength(150)
        .IsUnicode();

      builder.Property(history => history.MisfireTime)
        .HasColumnName("MISFIRE_TIME")
        .IsRequired();

      builder.Property(history => history.ScheduledTime)
        .HasColumnName("SCHED_TIME");

      builder.Property(history => history.Reason)
        .HasColumnName("REASON");

      builder.HasIndex(history => new { history.SchedulerName, history.MisfireTime })
        .HasDatabaseName($"IDX_{prefix}MH_MISFIRE_TIME");

      builder.HasIndex(history => new { history.SchedulerName, history.InstanceName })
        .HasDatabaseName($"IDX_{prefix}MH_INST");
    }
  }
}