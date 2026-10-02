using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AppAny.Quartz.EntityFrameworkCore.Migrations.MySql
{
  public class QuartzMisfireHistoryEntityTypeConfiguration : IEntityTypeConfiguration<QuartzMisfireHistory>
  {
    private readonly string? prefix;

    public QuartzMisfireHistoryEntityTypeConfiguration(string? prefix)
    {
      this.prefix = prefix;
    }

    public void Configure(EntityTypeBuilder<QuartzMisfireHistory> builder)
    {
      builder.ToTable($"{prefix}MISFIRE_HISTORY");

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

      builder.Property(history => history.TriggerName)
        .HasColumnName("TRIGGER_NAME")
        .HasColumnType("varchar(200)")
        .IsRequired();

      builder.Property(history => history.TriggerGroup)
        .HasColumnName("TRIGGER_GROUP")
        .HasColumnType("varchar(200)")
        .IsRequired();

      builder.Property(history => history.JobName)
        .HasColumnName("JOB_NAME")
        .HasColumnType("varchar(200)");

      builder.Property(history => history.JobGroup)
        .HasColumnName("JOB_GROUP")
        .HasColumnType("varchar(200)");

      builder.Property(history => history.MisfireTime)
        .HasColumnName("MISFIRE_TIME")
        .HasColumnType("bigint(19)")
        .IsRequired();

      builder.Property(history => history.ScheduledTime)
        .HasColumnName("SCHED_TIME")
        .HasColumnType("bigint(19)");

      builder.Property(history => history.Reason)
        .HasColumnName("REASON")
        .HasColumnType("integer");

      builder.HasIndex(history => new { history.SchedulerName, history.MisfireTime })
        .HasDatabaseName($"IDX_{prefix}MH_MISFIRE_TIME");

      builder.HasIndex(history => new { history.SchedulerName, history.InstanceName })
        .HasDatabaseName($"IDX_{prefix}MH_INST");
    }
  }
}