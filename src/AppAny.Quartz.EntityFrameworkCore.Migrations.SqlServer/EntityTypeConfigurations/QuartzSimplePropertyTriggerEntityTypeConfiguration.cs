namespace AppAny.Quartz.EntityFrameworkCore.Migrations.SqlServer.EntityTypeConfigurations
{
  using Microsoft.EntityFrameworkCore;
  using Microsoft.EntityFrameworkCore.Metadata.Builders;

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
      builder.ToTable($"{prefix}SIMPROP_TRIGGERS", schema);

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

      builder.Property(x => x.StringProperty1)
        .HasColumnName("STR_PROP_1")
        .HasMaxLength(512)
        .IsUnicode()
        .IsRequired();

      builder.Property(x => x.StringProperty2)
        .HasColumnName("STR_PROP_2")
        .HasMaxLength(512)
        .IsUnicode()
        .IsRequired();

      builder.Property(x => x.StringProperty3)
        .HasColumnName("STR_PROP_3")
        .HasMaxLength(512)
        .IsUnicode()
        .IsRequired();

      builder.Property(x => x.IntegerProperty1)
        .HasColumnName("INT_PROP_1");

      builder.Property(x => x.IntegerProperty2)
        .HasColumnName("INT_PROP_2");

      builder.Property(x => x.LongProperty1)
        .HasColumnName("LONG_PROP_1");

      builder.Property(x => x.LongProperty2)
        .HasColumnName("LONG_PROP_2");

      builder.Property(x => x.DecimalProperty1)
        .HasColumnName("DEC_PROP_1")
        .HasColumnType("numeric(13,4)");

      builder.Property(x => x.DecimalProperty2)
        .HasColumnName("DEC_PROP_2")
        .HasColumnType("numeric(13,4)");

      builder.Property(x => x.BooleanProperty1)
        .HasColumnName("BOOL_PROP_1");

      builder.Property(x => x.BooleanProperty2)
        .HasColumnName("BOOL_PROP_2");

      builder.Property(x => x.TimeZoneId)
        .HasColumnName("TIME_ZONE_ID")
        .HasMaxLength(80)
        .IsUnicode();

      builder.HasOne(x => x.Trigger)
        .WithMany(x => x.SimplePropertyTriggers)
        .HasForeignKey(x => new { x.SchedulerName, x.TriggerName, x.TriggerGroup })
        .OnDelete(DeleteBehavior.Cascade);
    }
  }
}
