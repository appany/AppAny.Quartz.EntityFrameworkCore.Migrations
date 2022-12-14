namespace AppAny.Quartz.EntityFrameworkCore.Migrations
{
  public class QuartzCronTrigger
  {
    public static readonly string TableName = "CRON_TRIGGERS";

    public string SchedulerName { get; set; } = null!;
    public string TriggerName { get; set; } = null!;
    public string TriggerGroup { get; set; } = null!;
    public string CronExpression { get; set; } = null!;
    public string? TimeZoneId { get; set; }

    public QuartzTrigger Trigger { get; set; } = null!;
  }
}
