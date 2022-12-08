using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppAny.Quartz.EntityFrameworkCore.Migrations;

namespace AppAny.Quartz.EntityFrameworkCore.Migrations.MySql
{
  public class QuartzSimpleTriggerEntityTypeConfiguration : IEntityTypeConfiguration<QuartzSimpleTrigger>
  {
    private readonly string? prefix;

    public QuartzSimpleTriggerEntityTypeConfiguration(string? prefix)
    {
      this.prefix = prefix;
    }

    public void Configure(EntityTypeBuilder<QuartzSimpleTrigger> builder)
    {
      builder.ToTable($"{prefix}simple_triggers");

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

      builder.Property(x => x.RepeatCount)
        .HasColumnName("repeat_count")
        .HasColumnType("bigint(7)")
        .IsRequired();

      builder.Property(x => x.RepeatInterval)
        .HasColumnName("repeat_interval")
        .HasColumnType("bigint(12)")
        .IsRequired();

      builder.Property(x => x.TimesTriggered)
        .HasColumnName("times_triggered")
        .HasColumnType("bigint(10)")
        .IsRequired();

      builder.HasOne(x => x.Trigger)
        .WithMany(x => x.SimpleTriggers)
        .HasForeignKey(x => new { x.SchedulerName, x.TriggerName, x.TriggerGroup })
        .OnDelete(DeleteBehavior.Cascade);
    }
  }
}
