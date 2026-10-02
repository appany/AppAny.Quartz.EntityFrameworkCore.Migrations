using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AppAny.Quartz.EntityFrameworkCore.Migrations.PostgreSQL
{
  public class QuartzPausedTriggerGroupEntityTypeConfiguration : IEntityTypeConfiguration<QuartzPausedTriggerGroup>
  {
    private readonly string? prefix;
    private readonly string? schema;

    public QuartzPausedTriggerGroupEntityTypeConfiguration(string? prefix, string? schema)
    {
      this.prefix = prefix;
      this.schema = schema;
    }

    public void Configure(EntityTypeBuilder<QuartzPausedTriggerGroup> builder)
    {
      builder.ToTable($"{prefix}paused_trigger_grps", schema);

      builder.HasKey(x => new { x.SchedulerName, x.TriggerGroup });

      builder.Property(x => x.SchedulerName)
        .HasColumnName("sched_name")
        .HasColumnType("text")
        .IsRequired();

      builder.Property(x => x.TriggerGroup)
        .HasColumnName("trigger_group")
        .HasColumnType("text")
        .IsRequired();

      builder.Property(group => group.PauseReason)
        .HasColumnName("pause_reason")
        .HasColumnType("varchar(250)");

      builder.Property(group => group.PausedBy)
        .HasColumnName("paused_by")
        .HasColumnType("varchar(200)");

      builder.Property(group => group.PausedAt)
        .HasColumnName("paused_at")
        .HasColumnType("bigint");
    }
  }
}
