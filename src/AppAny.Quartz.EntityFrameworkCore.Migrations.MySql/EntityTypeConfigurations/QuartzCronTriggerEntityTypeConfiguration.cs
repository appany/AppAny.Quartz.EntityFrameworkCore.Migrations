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
  public class QuartzCronTriggerEntityTypeConfiguration : IEntityTypeConfiguration<QuartzCronTrigger>
  {
    private readonly string? prefix;

    public QuartzCronTriggerEntityTypeConfiguration(string? prefix)
    {
      this.prefix = prefix;
    }

    public void Configure(EntityTypeBuilder<QuartzCronTrigger> builder)
    {
      builder.ToTable($"{prefix}cron_triggers");

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

      builder.Property(x => x.CronExpression)
        .HasColumnName("cron_expression")
        .HasColumnType("varchar(120)")
        .IsRequired();

      builder.Property(x => x.TimeZoneId)
        .HasColumnName("time_zone_id")
        .HasColumnType("varchar(80)");

      builder.HasOne(x => x.Trigger)
        .WithMany(x => x.CronTriggers)
        .HasForeignKey(x => new { x.SchedulerName, x.TriggerName, x.TriggerGroup })
        .OnDelete(DeleteBehavior.Cascade);
    }
  }
}
