using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AppAny.Quartz.EntityFrameworkCore.Migrations.SqlServer
{
  public class QuartzSimpleTriggerEntityTypeConfiguration : IEntityTypeConfiguration<QuartzSimpleTrigger>
  {
    private readonly string? prefix;
    private readonly string? schema;

    public QuartzSimpleTriggerEntityTypeConfiguration(string? prefix, string? schema)
    {
      this.prefix = prefix;
      this.schema = schema;
    }

    public void Configure(EntityTypeBuilder<QuartzSimpleTrigger> builder)
    {
      builder.ToTable($"{prefix}SIMPLE_TRIGGERS", schema);

      builder.HasKey(x => new { x.SchedulerName, x.TriggerName, x.TriggerGroup });

      builder.Property(x => x.SchedulerName)
        .HasColumnName("SCHED_NAME")
        .HasMaxLength(120)
        .IsUnicode()
        .IsRequired();

      builder.Property(x => x.TriggerName)
        .HasColumnName("TRIGGER_NAME")
        .HasMaxLength(150)
        .IsUnicode()
        .IsRequired();

      builder.Property(x => x.TriggerGroup)
        .HasColumnName("TRIGGER_GROUP")
        .HasMaxLength(150)
        .IsUnicode()
        .IsRequired();

      builder.Property(x => x.RepeatCount)
        .HasColumnName("REPEAT_COUNT")
        .IsRequired();

      builder.Property(x => x.RepeatInterval)
        .HasColumnName("REPEAT_INTERVAL")
        .IsRequired();

      builder.Property(x => x.TimesTriggered)
        .HasColumnName("TIMES_TRIGGERED")
        .IsRequired();

      builder.HasOne(x => x.Trigger)
        .WithMany(x => x.SimpleTriggers)
        .HasForeignKey(x => new { x.SchedulerName, x.TriggerName, x.TriggerGroup })
        .OnDelete(DeleteBehavior.Cascade);
    }
  }
}
