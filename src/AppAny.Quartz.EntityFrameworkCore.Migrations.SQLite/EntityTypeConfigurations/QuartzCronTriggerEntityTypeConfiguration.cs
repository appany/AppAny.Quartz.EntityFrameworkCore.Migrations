using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AppAny.Quartz.EntityFrameworkCore.Migrations.SQLite.EntityTypeConfigurations;

public class QuartzCronTriggerEntityTypeConfiguration : IEntityTypeConfiguration<QuartzCronTrigger>
{
  private readonly string? prefix;
  private readonly string? schema;

  public QuartzCronTriggerEntityTypeConfiguration(string? prefix, string? schema)
  {
    this.prefix = prefix;
    this.schema = schema;
  }

  public void Configure(EntityTypeBuilder<QuartzCronTrigger> builder)
  {
    builder.ToTable(prefix + "CRON_TRIGGERS", schema);

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

    builder.Property(x => x.CronExpression)
      .HasColumnName("CRON_EXPRESSION")
      .HasColumnType("text")
      .IsRequired();

    builder.Property(x => x.TimeZoneId)
      .HasColumnName("TIME_ZONE_ID")
      .HasColumnType("text");

    builder.HasOne(x => x.Trigger)
      .WithMany(x => x.CronTriggers)
      .HasForeignKey(x => new { x.SchedulerName, x.TriggerName, x.TriggerGroup })
      .OnDelete(DeleteBehavior.Cascade);
  }
}

