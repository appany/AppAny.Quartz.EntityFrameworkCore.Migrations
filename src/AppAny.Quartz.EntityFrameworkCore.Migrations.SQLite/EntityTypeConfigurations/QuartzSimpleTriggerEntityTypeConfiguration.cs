using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AppAny.Quartz.EntityFrameworkCore.Migrations.SQLite;

public class QuartzSimpleTriggerEntityTypeConfiguration : IEntityTypeConfiguration<QuartzSimpleTrigger>
{
  private readonly string _prefix;

  public QuartzSimpleTriggerEntityTypeConfiguration(string prefix)
  {
    this._prefix = prefix;
  }

  public void Configure(EntityTypeBuilder<QuartzSimpleTrigger> builder)
  {
    builder.ToTable(_prefix + "SIMPLE_TRIGGERS");

    builder.HasKey(x => new { x.SchedulerName, x.TriggerName, x.TriggerGroup });

    builder.Property(x => x.SchedulerName)
      .HasColumnName("SCHED_NAME")
      .HasColumnType("text")
      .IsRequired();

    builder.Property(x => x.TriggerName)
      .HasColumnName("TRIGGER_NAME")
      .HasColumnType("text")
      .IsRequired();

    builder.Property(x => x.TriggerGroup)
      .HasColumnName("TRIGGER_GROUP")
      .HasColumnType("text")
      .IsRequired();

    builder.Property(x => x.RepeatCount)
      .HasColumnName("REPEAT_COUNT")
      .HasColumnType("bigint")
      .IsRequired();

    builder.Property(x => x.RepeatInterval)
      .HasColumnName("REPEAT_INTERVAL")
      .HasColumnType("bigint")
      .IsRequired();

    builder.Property(x => x.TimesTriggered)
      .HasColumnName("TIMES_TRIGGERED")
      .HasColumnType("bigint")
      .IsRequired();

    builder.HasOne(x => x.Trigger)
      .WithMany(x => x.SimpleTriggers)
      .HasForeignKey(x => new { x.SchedulerName, x.TriggerName, x.TriggerGroup })
      .OnDelete(DeleteBehavior.Cascade);
  }
}

