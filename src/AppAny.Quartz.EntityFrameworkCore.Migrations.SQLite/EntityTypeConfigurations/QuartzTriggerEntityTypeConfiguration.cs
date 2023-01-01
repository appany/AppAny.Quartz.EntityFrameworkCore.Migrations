using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AppAny.Quartz.EntityFrameworkCore.Migrations.SQLite;

public class QuartzTriggerEntityTypeConfiguration : IEntityTypeConfiguration<QuartzTrigger>
{
  private readonly string _prefix;

  public QuartzTriggerEntityTypeConfiguration(string prefix)
  {
    this._prefix = prefix;
  }

  public void Configure(EntityTypeBuilder<QuartzTrigger> builder)
  {
    builder.ToTable(_prefix + "TRIGGERS");

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

    builder.Property(x => x.JobName)
      .HasColumnName("JOB_NAME")
      .HasColumnType("text")
      .IsRequired();

    builder.Property(x => x.JobGroup)
      .HasColumnName("JOB_GROUP")
      .HasColumnType("text")
      .IsRequired();

    builder.Property(x => x.Description)
      .HasColumnName("DESCRIPTION")
      .HasColumnType("text");

    builder.Property(x => x.NextFireTime)
      .HasColumnName("NEXT_FIRE_TIME")
      .HasColumnType("bigint");

    builder.Property(x => x.PreviousFireTime)
      .HasColumnName("PREV_FIRE_TIME")
      .HasColumnType("bigint");

    builder.Property(x => x.Priority)
      .HasColumnName("PRIORITY")
      .HasColumnType("integer");

    builder.Property(x => x.TriggerState)
      .HasColumnName("TRIGGER_STATE")
      .HasColumnType("text")
      .IsRequired();

    builder.Property(x => x.TriggerType)
      .HasColumnName("TRIGGER_TYPE")
      .HasColumnType("text")
      .IsRequired();

    builder.Property(x => x.StartTime)
      .HasColumnName("START_TIME")
      .HasColumnType("bigint")
      .IsRequired();

    builder.Property(x => x.EndTime)
      .HasColumnName("END_TIME")
      .HasColumnType("bigint");

    builder.Property(x => x.CalendarName)
      .HasColumnName("CALENDAR_NAME")
      .HasColumnType("text");

    builder.Property(x => x.MisfireInstruction)
      .HasColumnName("MISFIRE_INSTR")
      .HasColumnType("smallint");

    builder.Property(x => x.JobData)
      .HasColumnName("JOB_DATA")
      .HasColumnType("bytea");

    builder.HasOne(x => x.JobDetail)
      .WithMany(x => x.Triggers)
      .HasForeignKey(x => new { x.SchedulerName, x.JobName, x.JobGroup })
      .IsRequired();

    builder.HasIndex(x => x.NextFireTime)
      .HasDatabaseName($"IDX_{_prefix}T_NEXT_FIRE_TIME");

    builder.HasIndex(x => x.TriggerState)
      .HasDatabaseName($"IDX_{_prefix}T_STATE");

    builder.HasIndex(x => new { x.NextFireTime, x.TriggerState })
      .HasDatabaseName($"IDX_{_prefix}T_NFT_ST");
  }
}

