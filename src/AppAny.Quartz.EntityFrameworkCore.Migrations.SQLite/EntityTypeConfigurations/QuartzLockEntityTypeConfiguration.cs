using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AppAny.Quartz.EntityFrameworkCore.Migrations.SQLite.EntityTypeConfigurations;

public class QuartzLockEntityTypeConfiguration : IEntityTypeConfiguration<QuartzLock>
{
  private readonly string _prefix;
  private readonly string _schema;

  public QuartzLockEntityTypeConfiguration(string prefix, string schema)
  {
    this._prefix = prefix;
    this._schema = schema;
  }

  public void Configure(EntityTypeBuilder<QuartzLock> builder)
  {
    builder.ToTable(_prefix + "LOCKS", _schema);

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

