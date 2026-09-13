using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AppAny.Quartz.EntityFrameworkCore.Migrations.SQLite;

public class QuartzPausedJobGroupEntityTypeConfiguration : IEntityTypeConfiguration<QuartzPausedJobGroup>
{
  private readonly string _prefix;

  public QuartzPausedJobGroupEntityTypeConfiguration(string prefix)
  {
    this._prefix = prefix;
  }

  public void Configure(EntityTypeBuilder<QuartzPausedJobGroup> builder)
  {
    builder.ToTable(_prefix + "PAUSED_JOB_GRPS");

    builder.HasKey(x => new { x.SchedulerName, x.JobGroup });

    builder.Property(x => x.SchedulerName)
      .HasColumnName("SCHED_NAME")
      .HasColumnType("text")
      .IsRequired();

    builder.Property(x => x.JobGroup)
      .HasColumnName("JOB_GROUP")
      .HasColumnType("text")
      .IsRequired();
  }
}
