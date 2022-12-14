namespace AppAny.Quartz.EntityFrameworkCore.Migrations.SqlServer.EntityTypeConfigurations
{
  using Microsoft.EntityFrameworkCore;
  using Microsoft.EntityFrameworkCore.Metadata.Builders;

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
      builder.ToTable($"{prefix}CALENDARS", schema);

      builder.HasKey(x => new { x.SchedulerName, x.CalendarName });

      builder.Property(x => x.SchedulerName)
        .HasColumnName("SCHED_NAME")
        .HasMaxLength(120)
        .IsUnicode()
        .IsRequired();

      builder.Property(x => x.CalendarName)
        .HasColumnName("CALENDAR_NAME")
        .HasMaxLength(200)
        .IsUnicode()
        .IsRequired();

      builder.Property(x => x.Calendar)
        .HasColumnName("CALENDAR")
        .IsRequired();
    }
  }
}
