using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AppAny.Quartz.EntityFrameworkCore.Migrations.SQLite.EntityTypeConfigurations;

public class QuartzJobDetailEntityTypeConfiguration : IEntityTypeConfiguration<QuartzJobDetail>
{
  private readonly string? prefix;
  private readonly string? schema;

  public QuartzJobDetailEntityTypeConfiguration(string? prefix, string? schema)
  {
    this.prefix = prefix;
    this.schema = schema;
  }

  public void Configure(EntityTypeBuilder<QuartzJobDetail> builder)
  {
    builder.ToTable(prefix + "JOB_DETAILS", schema);

    builder.HasKey(x => new { x.SchedulerName, x.JobName, x.JobGroup });

    builder.Property(x => x.SchedulerName)
      .HasColumnName("SCHED_NAME")
      .HasColumnType("text")
      .IsRequired();

    builder.Property(x => x.JobName)
      .HasColumnName("JOB_NAME")
      .HasColumnType("text")
      .IsRequired();

    builder.Property(x => x.JobGroup)
      .HasColumnName("JOB_GROUP")
      .HasColumnType("text")
      .IsRequired();

    builder.Property(x => x.Description)
      .HasColumnName("DESCRIPTION")
      .HasColumnType("text");

    builder.Property(x => x.JobClassName)
      .HasColumnName("JOB_CLASS_NAME")
      .HasColumnType("text")
      .IsRequired();

    builder.Property(x => x.IsDurable)
      .HasColumnName("IS_DURABLE")
      .HasColumnType("bool")
      .IsRequired();

    builder.Property(x => x.IsNonConcurrent)
      .HasColumnName("IS_NONCONCURRENT")
      .HasColumnType("bool")
      .IsRequired();

    builder.Property(x => x.IsUpdateData)
      .HasColumnName("IS_UPDATE_DATA")
      .HasColumnType("bool")
      .IsRequired();

    builder.Property(x => x.RequestsRecovery)
      .HasColumnName("REQUESTS_RECOVERY")
      .HasColumnType("bool")
      .IsRequired();

    builder.Property(x => x.JobData)
      .HasColumnName("JOB_DATA")
      .HasColumnType("bytea");

    builder.HasIndex(x => x.RequestsRecovery)
      .HasDatabaseName($"IDX_{prefix}J_REQ_RECOVERY");
  }
}

