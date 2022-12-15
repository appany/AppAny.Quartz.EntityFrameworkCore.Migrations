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
    builder.ToTable(prefix + "LOCKS", schema);

    builder.HasKey(x => new { x.SchedulerName, x.LockName });

    builder.Property(x => x.SchedulerName)
      .HasColumnName("SCHED_NAME")
      .HasColumnType("text")
      .IsRequired();

    builder.Property(x => x.LockName)
      .HasColumnName("LOCK_NAME")
      .HasColumnType("text")
      .IsRequired();
  }
}

