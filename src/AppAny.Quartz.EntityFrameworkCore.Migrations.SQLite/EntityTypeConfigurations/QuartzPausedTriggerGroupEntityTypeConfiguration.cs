using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AppAny.Quartz.EntityFrameworkCore.Migrations.SQLite.EntityTypeConfigurations;

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
    builder.ToTable(prefix + QuartzPausedTriggerGroup.TableName, schema);

    builder.HasKey(x => new { x.SchedulerName, x.TriggerGroup });

    builder.Property(x => x.SchedulerName)
      .HasColumnName("sched_name")
      .HasColumnType("text")
      .IsRequired();

    builder.Property(x => x.TriggerGroup)
      .HasColumnName("trigger_group")
      .HasColumnType("text")
      .IsRequired();
  }
}

