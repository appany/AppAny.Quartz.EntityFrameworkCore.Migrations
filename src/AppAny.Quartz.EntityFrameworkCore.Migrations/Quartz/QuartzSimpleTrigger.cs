namespace AppAny.Quartz.EntityFrameworkCore.Migrations
{
  public class QuartzSimpleTrigger
  {
    public static readonly string TableName = "SIMPLE_TRIGGERS";

    public string SchedulerName { get; set; } = null!;
    public string TriggerName { get; set; } = null!;
    public string TriggerGroup { get; set; } = null!;
    public long RepeatCount { get; set; }
    public long RepeatInterval { get; set; }
    public long TimesTriggered { get; set; }

    public QuartzTrigger Trigger { get; set; } = null!;
  }
}
