using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AppAny.Quartz.EntityFrameworkCore.Migrations.SQLite.EntityTypeConfigurations;

public class QuartzJobDetailEntityTypeConfiguration : IEntityTypeConfiguration<QuartzJobDetail>
{
  private readonly string _prefix;
  private readonly string _schema;

  public QuartzJobDetailEntityTypeConfiguration(string prefix, string schema)
  {
    this._prefix = prefix;
    this._schema = schema;
  }

  public void Configure(EntityTypeBuilder<QuartzJobDetail> builder)
  {
    builder.ToTable(_prefix + "JOB_DETAILS", _schema);

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
      .HasDatabaseName($"IDX_{_prefix}J_REQ_RECOVERY");
  }
}

