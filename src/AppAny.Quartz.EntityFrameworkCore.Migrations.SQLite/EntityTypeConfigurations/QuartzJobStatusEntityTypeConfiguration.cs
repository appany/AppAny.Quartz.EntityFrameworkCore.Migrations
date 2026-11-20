using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AppAny.Quartz.EntityFrameworkCore.Migrations.SQLite;

public class QuartzJobStatusEntityTypeConfiguration : IEntityTypeConfiguration<QuartzJobStatus>
{
  private readonly string _prefix;

  public QuartzJobStatusEntityTypeConfiguration(string prefix)
  {
    this._prefix = prefix;
  }

  public void Configure(EntityTypeBuilder<QuartzJobStatus> builder)
  {
    builder.ToTable(_prefix + "JOB_STATUS");

    builder.HasKey(status => new { status.SchedulerName, status.JobGroup, status.JobName });

    builder.Property(status => status.SchedulerName)
      .HasColumnName("SCHED_NAME")
      .HasColumnType("text")
      .IsRequired();

    builder.Property(status => status.JobGroup)
      .HasColumnName("JOB_GROUP")
      .HasColumnType("text")
      .IsRequired();

    builder.Property(status => status.JobName)
      .HasColumnName("JOB_NAME")
      .HasColumnType("text")
      .IsRequired();

    builder.Property(status => status.FirstFiredTime)
      .HasColumnName("FIRST_FIRED_TIME")
      .HasColumnType("bigint")
      .IsRequired();

    builder.Property(status => status.LastFiredTime)
      .HasColumnName("LAST_FIRED_TIME")
      .HasColumnType("bigint")
      .IsRequired();

    builder.Property(status => status.LastResult)
      .HasColumnName("LAST_RESULT")
      .HasColumnType("integer")
      .IsRequired();

    builder.Property(status => status.LastRunTime)
      .HasColumnName("LAST_RUN_TIME")
      .HasColumnType("bigint")
      .IsRequired();

    builder.Property(status => status.LastInstanceName)
      .HasColumnName("LAST_INSTANCE_NAME")
      .HasColumnType("text")
      .IsRequired();

    builder.Property(status => status.LastEntryId)
      .HasColumnName("LAST_ENTRY_ID")
      .HasColumnType("text");

    builder.Property(status => status.LastSummary)
      .HasColumnName("LAST_SUMMARY")
      .HasColumnType("text");

    builder.Property(status => status.LastSuccessTime)
      .HasColumnName("LAST_SUCCESS_TIME")
      .HasColumnType("bigint");

    builder.Property(status => status.LastFailureTime)
      .HasColumnName("LAST_FAILURE_TIME")
      .HasColumnType("bigint");

    builder.Property(status => status.LastFailureMessage)
      .HasColumnName("LAST_FAILURE_MESSAGE")
      .HasColumnType("text");

    builder.Property(status => status.ConsecutiveFailures)
      .HasColumnName("CONSECUTIVE_FAILURES")
      .HasColumnType("integer")
      .HasDefaultValue(0)
      .IsRequired();

    builder.Property(status => status.RunCount)
      .HasColumnName("RUN_COUNT")
      .HasColumnType("bigint")
      .HasDefaultValue(0L)
      .IsRequired();

    builder.Property(status => status.FailureCount)
      .HasColumnName("FAILURE_COUNT")
      .HasColumnType("bigint")
      .HasDefaultValue(0L)
      .IsRequired();
  }
}