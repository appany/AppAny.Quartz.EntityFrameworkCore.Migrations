namespace AppAny.Quartz.EntityFrameworkCore.Migrations
{
  public class QuartzCalendar
  {
    public static readonly string TableName = "CALENDARS";

    public string SchedulerName { get; set; } = null!;
    public string CalendarName { get; set; } = null!;
    public byte[] Calendar { get; set; } = null!;
  }
}
