using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AppAny.Quartz.EntityFrameworkCore.Migrations.SQLite.EntityTypeConfigurations;

public class QuartzLockEntityTypeConfiguration : IEntityTypeConfiguration<QuartzLock>
{
  private readonly string? prefix;
  private readonly string? schema;

  public QuartzLockEntityTypeConfiguration(string? prefix, string? schema)
  {
    this.prefix = prefix;
    this.schema = schema;
  }

  public void Configure(EntityTypeBuilder<QuartzLock> builder)
  {
    builder.ToTable($"{prefix}LOCKS");

    builder.HasKey(x => new { x.SchedulerName, x.LockName });

    builder.Property(x => x.SchedulerName)
      .HasColumnName("sched_name")
      .HasColumnType("text")
      .IsRequired();

    builder.Property(x => x.LockName)
      .HasColumnName("lock_name")
      .HasColumnType("text")
      .IsRequired();
  }
}

