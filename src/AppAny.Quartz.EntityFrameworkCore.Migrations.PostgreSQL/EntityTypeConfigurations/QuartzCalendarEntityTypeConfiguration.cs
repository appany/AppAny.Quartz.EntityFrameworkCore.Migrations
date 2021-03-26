using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AppAny.Quartz.EntityFrameworkCore.Migrations.PostgreSQL
{
  public class QuartzCalendarEntityTypeConfiguration : IEntityTypeConfiguration<QuartzCalendar>
  {
    private readonly string? prefix;
    private readonly string? schema;

    public QuartzCalendarEntityTypeConfiguration(string? prefix, string? schema)
    {
      this.prefix = prefix;
      this.schema = schema;
    }

    public void Configure(EntityTypeBuilder<QuartzCalendar> builder)
    {
      builder.ToTable($"{prefix}calendars", schema);

      builder.HasKey(x => new { x.SchedulerName, x.CalendarName });

      builder.Property(x => x.SchedulerName)
        .HasColumnName("sched_name")
        .HasColumnType("text")
        .IsRequired();

      builder.Property(x => x.CalendarName)
        .HasColumnName("calendar_name")
        .HasColumnType("text")
        .IsRequired();

      builder.Property(x => x.Calendar)
        .HasColumnName("calendar")
        .HasColumnType("bytea")
        .IsRequired();
    }
  }
}
