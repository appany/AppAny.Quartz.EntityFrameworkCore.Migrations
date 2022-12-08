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
  public class QuartzSimplePropertyTriggerEntityTypeConfiguration : IEntityTypeConfiguration<QuartzSimplePropertyTrigger>
  {
    private readonly string? prefix;

    public QuartzSimplePropertyTriggerEntityTypeConfiguration(string? prefix)
    {
      this.prefix = prefix;
    }

    public void Configure(EntityTypeBuilder<QuartzSimplePropertyTrigger> builder)
    {
      builder.ToTable($"{prefix}SIMPROP_TRIGGERS");

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

      builder.Property(x => x.StringProperty1)
        .HasColumnName("str_prop_1")
        .HasColumnType("varchar(512)");

      builder.Property(x => x.StringProperty2)
        .HasColumnName("str_prop_2")
        .HasColumnType("varchar(512)");

      builder.Property(x => x.StringProperty3)
        .HasColumnName("str_prop_3")
        .HasColumnType("varchar(512)");

      builder.Property(x => x.IntegerProperty1)
        .HasColumnName("int_prop_1")
        .HasColumnType("int");

      builder.Property(x => x.IntegerProperty2)
        .HasColumnName("int_prop_2")
        .HasColumnType("int");

      builder.Property(x => x.LongProperty1)
        .HasColumnName("long_prop_1")
        .HasColumnType("BIGINT");

      builder.Property(x => x.LongProperty2)
        .HasColumnName("long_prop_2")
        .HasColumnType("BIGINT");

      builder.Property(x => x.DecimalProperty1)
        .HasColumnName("dec_prop_1")
        .HasColumnType("NUMERIC(13,4)");

      builder.Property(x => x.DecimalProperty2)
        .HasColumnName("dec_prop_2")
        .HasColumnType("NUMERIC(13,4)");

      builder.Property(x => x.BooleanProperty1)
        .HasColumnName("bool_prop_1")
        .HasColumnType("tinyint(1)");

      builder.Property(x => x.BooleanProperty2)
        .HasColumnName("bool_prop_2")
        .HasColumnType("tinyint(1)");

      builder.Property(x => x.TimeZoneId)
        .HasColumnName("time_zone_id")
        .HasColumnType("varchar(80)");

      builder.HasOne(x => x.Trigger)
        .WithMany(x => x.SimplePropertyTriggers)
        .HasForeignKey(x => new { x.SchedulerName, x.TriggerName, x.TriggerGroup })
        .OnDelete(DeleteBehavior.Cascade);
    }
  }
}
