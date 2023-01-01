using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AppAny.Quartz.EntityFrameworkCore.Migrations.SQLite;

public class QuartzCalendarEntityTypeConfiguration : IEntityTypeConfiguration<QuartzCalendar>
{
  private readonly string _prefix;

  public QuartzCalendarEntityTypeConfiguration(string prefix)
  {
    this._prefix = prefix;
  }

  public void Configure(EntityTypeBuilder<QuartzCalendar> builder)
  {
    builder.ToTable(_prefix + "CALENDARS");

    builder.HasKey(x => new { x.SchedulerName, x.CalendarName });

    builder.Property(x => x.SchedulerName)
      .HasColumnName("SCHED_NAME")
      .HasColumnType("text")
      .IsRequired();

    builder.Property(x => x.CalendarName)
      .HasColumnName("CALENDAR_NAME")
      .HasColumnType("text")
      .IsRequired();

    builder.Property(x => x.Calendar)
      .HasColumnName("CALENDAR")
      .HasColumnType("bytea")
      .IsRequired();
  }
}

