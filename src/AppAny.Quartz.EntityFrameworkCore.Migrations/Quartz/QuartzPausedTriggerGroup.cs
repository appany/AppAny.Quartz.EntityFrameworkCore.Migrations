namespace AppAny.Quartz.EntityFrameworkCore.Migrations
{
  public class QuartzPausedTriggerGroup
  {
    public static readonly string TableName = "PAUSED_TRIGGER_GRPS";

    public string SchedulerName { get; set; } = null!;
    public string TriggerGroup { get; set; } = null!;
  }
}
