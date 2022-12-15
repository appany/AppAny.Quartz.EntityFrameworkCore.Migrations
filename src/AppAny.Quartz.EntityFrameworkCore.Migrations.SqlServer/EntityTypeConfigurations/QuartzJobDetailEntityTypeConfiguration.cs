namespace AppAny.Quartz.EntityFrameworkCore.Migrations.SqlServer
{
  using Microsoft.EntityFrameworkCore;
  using Microsoft.EntityFrameworkCore.Metadata.Builders;

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
      builder.ToTable($"{prefix}JOB_DETAILS", schema);

      builder.HasKey(x => new { x.SchedulerName, x.JobName, x.JobGroup });

      builder.Property(x => x.SchedulerName)
        .HasColumnName("SCHED_NAME")
        .HasMaxLength(120)
        .IsUnicode()
        .IsRequired();

      builder.Property(x => x.JobName)
        .HasColumnName("JOB_NAME")
        .HasMaxLength(150)
        .IsUnicode()
        .IsRequired();

      builder.Property(x => x.JobGroup)
        .HasColumnName("JOB_GROUP")
        .HasMaxLength(150)
        .IsUnicode()
        .IsRequired();

      builder.Property(x => x.Description)
        .HasColumnName("DESCRIPTION")
        .HasMaxLength(250)
        .IsUnicode();

      builder.Property(x => x.JobClassName)
        .HasColumnName("JOB_CLASS_NAME")
        .HasMaxLength(250)
        .IsUnicode()
        .IsRequired();

      builder.Property(x => x.IsDurable)
        .HasColumnName("IS_DURABLE")
        .IsRequired();

      builder.Property(x => x.IsNonConcurrent)
        .HasColumnName("IS_NONCONCURRENT")
        .IsRequired();

      builder.Property(x => x.IsUpdateData)
        .HasColumnName("IS_UPDATE_DATA")
        .IsRequired();

      builder.Property(x => x.RequestsRecovery)
        .HasColumnName("REQUESTS_RECOVERY")
        .IsRequired();

      builder.Property(x => x.JobData)
        .HasColumnName("JOB_DATA");

      builder.HasIndex(x => x.RequestsRecovery)
        .HasDatabaseName($"IDX_{prefix}J_REQ_RECOVERY");
    }
  }
}
