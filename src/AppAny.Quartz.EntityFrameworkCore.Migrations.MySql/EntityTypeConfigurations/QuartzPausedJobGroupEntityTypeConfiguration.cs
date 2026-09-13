using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AppAny.Quartz.EntityFrameworkCore.Migrations.MySql
{
  public class QuartzPausedJobGroupEntityTypeConfiguration : IEntityTypeConfiguration<QuartzPausedJobGroup>
  {
    private readonly string? prefix;

    public QuartzPausedJobGroupEntityTypeConfiguration(string? prefix)
    {
      this.prefix = prefix;
    }

    public void Configure(EntityTypeBuilder<QuartzPausedJobGroup> builder)
    {
      builder.ToTable($"{prefix}PAUSED_JOB_GRPS");

      builder.HasKey(x => new { x.SchedulerName, x.JobGroup });

      builder.Property(x => x.SchedulerName)
        .HasColumnName("SCHED_NAME")
        .HasColumnType("varchar(120)")
        .IsRequired();

      builder.Property(x => x.JobGroup)
        .HasColumnName("JOB_GROUP")
        .HasColumnType("varchar(200)")
        .IsRequired();
    }
  }
}
