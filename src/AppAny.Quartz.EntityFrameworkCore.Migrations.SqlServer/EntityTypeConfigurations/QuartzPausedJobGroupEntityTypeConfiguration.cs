using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AppAny.Quartz.EntityFrameworkCore.Migrations.SqlServer
{
  public class QuartzPausedJobGroupEntityTypeConfiguration : IEntityTypeConfiguration<QuartzPausedJobGroup>
  {
    private readonly string? prefix;
    private readonly string? schema;

    public QuartzPausedJobGroupEntityTypeConfiguration(string? prefix, string? schema)
    {
      this.prefix = prefix;
      this.schema = schema;
    }

    public void Configure(EntityTypeBuilder<QuartzPausedJobGroup> builder)
    {
      builder.ToTable($"{prefix}PAUSED_JOB_GRPS", schema);

      builder.HasKey(x => new { x.SchedulerName, x.JobGroup });

      builder.Property(x => x.SchedulerName)
        .HasColumnName("SCHED_NAME")
        .HasMaxLength(120)
        .IsUnicode()
        .IsRequired();

      builder.Property(x => x.JobGroup)
        .HasColumnName("JOB_GROUP")
        .HasMaxLength(150)
        .IsUnicode()
        .IsRequired();
    }
  }
}
