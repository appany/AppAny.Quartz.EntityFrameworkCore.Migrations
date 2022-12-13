namespace AppAny.Quartz.EntityFrameworkCore.Migrations.SQL.EntityTypeConfigurations
{
  using Microsoft.EntityFrameworkCore;
  using Microsoft.EntityFrameworkCore.Metadata.Builders;

  public class QuartzTriggerEntityTypeConfiguration : IEntityTypeConfiguration<QuartzTrigger>
  {
    private readonly string? prefix;
    private readonly string? schema;

    public QuartzTriggerEntityTypeConfiguration(string? prefix, string? schema)
    {
      this.prefix = prefix;
      this.schema = schema;
    }

    public void Configure(EntityTypeBuilder<QuartzTrigger> builder)
    {
      builder.ToTable($"{prefix}TRIGGERS", schema);

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

      builder.Property(x => x.JobName)
        .HasColumnName("JOB_NAME")
        .HasMaxLength(150)
        .IsUnicode()
        .IsRequired();

      builder.Property(x => x.JobGroup)
        .HasColumnName("JOB_GROUP")
        .HasMaxLength(150)
        .IsUnicode()
        .IsRequired();

      builder.Property(x => x.Description)
        .HasColumnName("DESCRIPTION")
        .HasMaxLength(250)
        .IsUnicode();

      builder.Property(x => x.NextFireTime)
        .HasColumnName("NEXT_FIRE_TIME");

      builder.Property(x => x.PreviousFireTime)
        .HasColumnName("PREV_FIRE_TIME");

      builder.Property(x => x.Priority)
        .HasColumnName("PRIORITY");

      builder.Property(x => x.TriggerState)
        .HasColumnName("TRIGGER_STATE")
        .HasMaxLength(16)
        .IsUnicode()
        .IsRequired();

      builder.Property(x => x.TriggerType)
        .HasColumnName("TRIGGER_TYPE")
        .HasMaxLength(8)
        .IsUnicode()
        .IsRequired();

      builder.Property(x => x.StartTime)
        .HasColumnName("START_TIME")
        .IsRequired();

      builder.Property(x => x.EndTime)
        .HasColumnName("END_TIME")
        .HasColumnType("bigint");

      builder.Property(x => x.CalendarName)
        .HasColumnName("CALENDAR_NAME")
        .HasMaxLength(200)
        .IsUnicode();

      builder.Property(x => x.MisfireInstruction)
        .HasColumnName("MISFIRE_INSTR");

      builder.Property(x => x.JobData)
        .HasColumnName("JOB_DATA");

      builder.HasOne(x => x.JobDetail)
        .WithMany(x => x.Triggers)
        .HasForeignKey(x => new { x.SchedulerName, x.JobName, x.JobGroup })
        .IsRequired();

      builder.HasIndex(x => x.NextFireTime)
        .HasDatabaseName($"IDX_{prefix}T_NEXT_FIRE_TIME");

      builder.HasIndex(x => x.TriggerState)
        .HasDatabaseName($"IDX_{prefix}T_STATE");

      builder.HasIndex(x => new { x.NextFireTime, x.TriggerState })
        .HasDatabaseName($"IDX_{prefix}T_NFT_ST");
    }
  }
}
