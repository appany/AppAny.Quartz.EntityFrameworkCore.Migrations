using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AppAny.Quartz.EntityFrameworkCore.Migrations.SqlServer
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
      builder.ToTable($"{prefix}JOB_STATUS", schema);

      builder.HasKey(status => new { status.SchedulerName, status.JobGroup, status.JobName });

      builder.Property(status => status.SchedulerName)
        .HasColumnName("SCHED_NAME")
        .HasMaxLength(120)
        .IsUnicode()
        .IsRequired();

      builder.Property(status => status.JobGroup)
        .HasColumnName("JOB_GROUP")
        .HasMaxLength(150)
        .IsUnicode()
        .IsRequired();

      builder.Property(status => status.JobName)
        .HasColumnName("JOB_NAME")
        .HasMaxLength(150)
        .IsUnicode()
        .IsRequired();

      builder.Property(status => status.FirstFiredTime)
        .HasColumnName("FIRST_FIRED_TIME")
        .IsRequired();

      builder.Property(status => status.LastFiredTime)
        .HasColumnName("LAST_FIRED_TIME")
        .IsRequired();

      builder.Property(status => status.LastResult)
        .HasColumnName("LAST_RESULT")
        .IsRequired();

      builder.Property(status => status.LastRunTime)
        .HasColumnName("LAST_RUN_TIME")
        .IsRequired();

      builder.Property(status => status.LastInstanceName)
        .HasColumnName("LAST_INSTANCE_NAME")
        .HasMaxLength(200)
        .IsUnicode()
        .IsRequired();

      builder.Property(status => status.LastEntryId)
        .HasColumnName("LAST_ENTRY_ID")
        .HasMaxLength(140)
        .IsUnicode();

      builder.Property(status => status.LastSummary)
        .HasColumnName("LAST_SUMMARY")
        .HasMaxLength(1000)
        .IsUnicode();

      builder.Property(status => status.LastSuccessTime)
        .HasColumnName("LAST_SUCCESS_TIME");

      builder.Property(status => status.LastFailureTime)
        .HasColumnName("LAST_FAILURE_TIME");

      builder.Property(status => status.LastFailureMessage)
        .HasColumnName("LAST_FAILURE_MESSAGE")
        .HasMaxLength(1000)
        .IsUnicode();

      builder.Property(status => status.ConsecutiveFailures)
        .HasColumnName("CONSECUTIVE_FAILURES")
        .HasDefaultValue(0)
        .IsRequired();

      builder.Property(status => status.RunCount)
        .HasColumnName("RUN_COUNT")
        .HasDefaultValue(0L)
        .IsRequired();

      builder.Property(status => status.FailureCount)
        .HasColumnName("FAILURE_COUNT")
        .HasDefaultValue(0L)
        .IsRequired();
    }
  }
}