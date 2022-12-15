using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AppAny.Quartz.EntityFrameworkCore.Migrations.MySql
{
  public class QuartzPausedTriggerGroupEntityTypeConfiguration : IEntityTypeConfiguration<QuartzPausedTriggerGroup>
  {
    private readonly string? prefix;

    public QuartzPausedTriggerGroupEntityTypeConfiguration(string? prefix)
    {
      this.prefix = prefix;
    }

    public void Configure(EntityTypeBuilder<QuartzPausedTriggerGroup> builder)
    {
      builder.ToTable($"{prefix}paused_trigger_grps");

      builder.HasKey(x => new { x.SchedulerName, x.TriggerGroup });

      builder.Property(x => x.SchedulerName)
        .HasColumnName("sched_name")
        .HasColumnType("varchar(120)")
        .IsRequired();

      builder.Property(x => x.TriggerGroup)
        .HasColumnName("trigger_group")
        .HasColumnType("varchar(200)")
        .IsRequired();
    }
  }
}
