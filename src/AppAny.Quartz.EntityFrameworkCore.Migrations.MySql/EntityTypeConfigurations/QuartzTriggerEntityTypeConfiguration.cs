using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppAny.Quartz.EntityFrameworkCore.Migrations.Quartz;

namespace AppAny.Quartz.EntityFrameworkCore.Migrations.MySql.EntityTypeConfigurations
{
  public class QuartzTriggerEntityTypeConfiguration : IEntityTypeConfiguration<QuartzTrigger>
  {
    private readonly string? prefix;

    public QuartzTriggerEntityTypeConfiguration(string? prefix)
    {
      this.prefix = prefix;
    }

    public void Configure(EntityTypeBuilder<QuartzTrigger> builder)
    {
      builder.ToTable($"{prefix}triggers");

      builder.HasKey(x => new { x.SchedulerName, x.TriggerName, x.TriggerGroup });

      builder.Property(x => x.SchedulerName)
        .HasColumnName("sched_name")
        .HasColumnType("varchar(120)")
        .IsRequired();

      builder.Property(x => x.TriggerName)
        .HasColumnName("trigger_name")
        .HasColumnType("varchar(200)")
        .IsRequired();

      builder.Property(x => x.TriggerGroup)
        .HasColumnName("trigger_group")
        .HasColumnType("varchar(200)")
        .IsRequired();

      builder.Property(x => x.JobName)
        .HasColumnName("job_name")
        .HasColumnType("varchar(200)")
        .IsRequired();

      builder.Property(x => x.JobGroup)
        .HasColumnName("job_group")
        .HasColumnType("varchar(200)")
        .IsRequired();

      builder.Property(x => x.Description)
        .HasColumnName("description")
        .HasColumnType("varchar(250)");

      builder.Property(x => x.NextFireTime)
        .HasColumnName("next_fire_time")
        .HasColumnType("bigint(19)");

      builder.Property(x => x.PreviousFireTime)
        .HasColumnName("prev_fire_time")
        .HasColumnType("bigint(19)");

      builder.Property(x => x.Priority)
        .HasColumnName("priority")
        .HasColumnType("integer");

      builder.Property(x => x.TriggerState)
        .HasColumnName("trigger_state")
        .HasColumnType("varchar(16)")
        .IsRequired();

      builder.Property(x => x.TriggerType)
        .HasColumnName("trigger_type")
        .HasColumnType("varchar(8)")
        .IsRequired();

      builder.Property(x => x.StartTime)
        .HasColumnName("start_time")
        .HasColumnType("bigint(19)")
        .IsRequired();

      builder.Property(x => x.EndTime)
        .HasColumnName("end_time")
        .HasColumnType("bigint(19)");

      builder.Property(x => x.CalendarName)
        .HasColumnName("calendar_name")
        .HasColumnType("varchar(200)");

      builder.Property(x => x.MisfireInstruction)
        .HasColumnName("misfire_instr")
        .HasColumnType("smallint(2)");

      builder.Property(x => x.JobData)
        .HasColumnName("job_data")
        .HasColumnType("blob");

      builder.HasOne(x => x.JobDetail)
        .WithMany(x => x.Triggers)
        .HasForeignKey(x => new { x.SchedulerName, x.JobName, x.JobGroup })
        .IsRequired();

      builder.HasIndex(x => x.NextFireTime)
        .HasDatabaseName($"idx_{prefix}t_next_fire_time");

      builder.HasIndex(x => x.TriggerState)
        .HasDatabaseName($"idx_{prefix}t_state");

      builder.HasIndex(x => new { x.NextFireTime, x.TriggerState })
        .HasDatabaseName($"idx_{prefix}t_nft_st");
    }
  }
}
