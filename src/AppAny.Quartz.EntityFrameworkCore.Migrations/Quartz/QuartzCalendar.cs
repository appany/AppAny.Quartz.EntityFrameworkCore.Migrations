namespace AppAny.Quartz.EntityFrameworkCore.Migrations.PostgreSQL
{
  public class QuartzCalendar
  {
    public string SchedulerName { get; set; } = null!;
    public string CalendarName { get; set; } = null!;
    public byte[] Calendar { get; set; } = null!;
  }
}
