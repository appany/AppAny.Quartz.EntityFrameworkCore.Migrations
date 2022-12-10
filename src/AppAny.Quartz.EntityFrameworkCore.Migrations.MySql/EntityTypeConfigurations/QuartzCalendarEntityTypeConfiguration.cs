using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppAny.Quartz.EntityFrameworkCore.Migrations;

namespace AppAny.Quartz.EntityFrameworkCore.Migrations.MySql
{
  public class QuartzCalendarEntityTypeConfiguration : IEntityTypeConfiguration<QuartzCalendar>
  {
    private readonly string? prefix;

    public QuartzCalendarEntityTypeConfiguration(string? prefix)
    {
      this.prefix = prefix;
    }

    public void Configure(EntityTypeBuilder<QuartzCalendar> builder)
    {
      builder.ToTable($"{prefix}calendars");

      builder.HasKey(x => new { x.SchedulerName, x.CalendarName });

      builder.Property(x => x.SchedulerName)
        .HasColumnName("sched_name")
        .HasColumnType("varchar(120)")
        .IsRequired();

      builder.Property(x => x.CalendarName)
        .HasColumnName("calendar_name")
        .HasColumnType("varchar(200)")
        .IsRequired();

      builder.Property(x => x.Calendar)
        .HasColumnName("calendar")
        .HasColumnType("blob")
        .IsRequired();
    }
  }
}
