using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AppAny.Quartz.EntityFrameworkCore.Migrations.PostgreSQL
{
  public class QuartzJobStatusEntityTypeConfiguration : IEntityTypeConfiguration<QuartzJobStatus>
  {
    private readonly string? prefix;
    private readonly string? schema;

    public QuartzJobStatusEntityTypeConfiguration(string? prefix, string? schema)
    {
      this.prefix = prefix;
      this.schema = schema;
    }

    public void Configure(EntityTypeBuilder<QuartzJobStatus> builder)
    {
      builder.ToTable($"{prefix}job_status", schema);

      builder.HasKey(status => new { status.SchedulerName, status.JobGroup, status.JobName });

      builder.Property(status => status.SchedulerName)
        .HasColumnName("sched_name")
        .HasColumnType("text")
        .IsRequired();

      builder.Property(status => status.JobGroup)
        .HasColumnName("job_group")
        .HasColumnType("text")
        .IsRequired();

      builder.Property(status => status.JobName)
        .HasColumnName("job_name")
        .HasColumnType("text")
        .IsRequired();

      builder.Property(status => status.FirstFiredTime)
        .HasColumnName("first_fired_time")
        .HasColumnType("bigint")
        .IsRequired();

      builder.Property(status => status.LastFiredTime)
        .HasColumnName("last_fired_time")
        .HasColumnType("bigint")
        .IsRequired();

      builder.Property(status => status.LastResult)
        .HasColumnName("last_result")
        .HasColumnType("integer")
        .IsRequired();

      builder.Property(status => status.LastRunTime)
        .HasColumnName("last_run_time")
        .HasColumnType("bigint")
        .IsRequired();

      builder.Property(status => status.LastInstanceName)
        .HasColumnName("last_instance_name")
        .HasColumnType("text")
        .IsRequired();

      builder.Property(status => status.LastEntryId)
        .HasColumnName("last_entry_id")
        .HasColumnType("text");

      builder.Property(status => status.LastSummary)
        .HasColumnName("last_summary")
        .HasColumnType("text");

      builder.Property(status => status.LastSuccessTime)
        .HasColumnName("last_success_time")
        .HasColumnType("bigint");

      builder.Property(status => status.LastFailureTime)
        .HasColumnName("last_failure_time")
        .HasColumnType("bigint");

      builder.Property(status => status.LastFailureMessage)
        .HasColumnName("last_failure_message")
        .HasColumnType("text");

      builder.Property(status => status.ConsecutiveFailures)
        .HasColumnName("consecutive_failures")
        .HasColumnType("integer")
        .HasDefaultValue(0)
        .IsRequired();

      builder.Property(status => status.RunCount)
        .HasColumnName("run_count")
        .HasColumnType("bigint")
        .HasDefaultValue(0L)
        .IsRequired();

      builder.Property(status => status.FailureCount)
        .HasColumnName("failure_count")
        .HasColumnType("bigint")
        .HasDefaultValue(0L)
        .IsRequired();
    }
  }
}