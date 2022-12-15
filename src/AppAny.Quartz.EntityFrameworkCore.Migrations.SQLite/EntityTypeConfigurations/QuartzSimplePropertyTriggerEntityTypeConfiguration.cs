using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AppAny.Quartz.EntityFrameworkCore.Migrations.SQLite.EntityTypeConfigurations;

public class QuartzSimplePropertyTriggerEntityTypeConfiguration
  : IEntityTypeConfiguration<QuartzSimplePropertyTrigger>
{
  private readonly string? prefix;
  private readonly string? schema;

  public QuartzSimplePropertyTriggerEntityTypeConfiguration(string? prefix, string? schema)
  {
    this.prefix = prefix;
    this.schema = schema;
  }

  public void Configure(EntityTypeBuilder<QuartzSimplePropertyTrigger> builder)
  {
    builder.ToTable(prefix + "SIMPROP_TRIGGERS", schema);

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

    builder.Property(x => x.StringProperty1)
      .HasColumnName("STR_PROP_1")
      .HasColumnType("text");

    builder.Property(x => x.StringProperty2)
      .HasColumnName("STR_PROP_2")
      .HasColumnType("text");

    builder.Property(x => x.StringProperty3)
      .HasColumnName("STR_PROP_3")
      .HasColumnType("text");

    builder.Property(x => x.IntegerProperty1)
      .HasColumnName("INT_PROP_1")
      .HasColumnType("integer");

    builder.Property(x => x.IntegerProperty2)
      .HasColumnName("INT_PROP_2")
      .HasColumnType("integer");

    builder.Property(x => x.LongProperty1)
      .HasColumnName("LONG_PROP_1")
      .HasColumnType("bigint");

    builder.Property(x => x.LongProperty2)
      .HasColumnName("LONG_PROP_2")
      .HasColumnType("bigint");

    builder.Property(x => x.DecimalProperty1)
      .HasColumnName("DEC_PROP_1")
      .HasColumnType("numeric");

    builder.Property(x => x.DecimalProperty2)
      .HasColumnName("DEC_PROP_2")
      .HasColumnType("numeric");

    builder.Property(x => x.BooleanProperty1)
      .HasColumnName("BOOL_PROP_1")
      .HasColumnType("bool");

    builder.Property(x => x.BooleanProperty2)
      .HasColumnName("BOOL_PROP_2")
      .HasColumnType("bool");

    builder.Property(x => x.TimeZoneId)
      .HasColumnName("TIME_ZONE_ID")
      .HasColumnType("text");

    builder.HasOne(x => x.Trigger)
      .WithMany(x => x.SimplePropertyTriggers)
      .HasForeignKey(x => new { x.SchedulerName, x.TriggerName, x.TriggerGroup })
      .OnDelete(DeleteBehavior.Cascade);
  }
}

