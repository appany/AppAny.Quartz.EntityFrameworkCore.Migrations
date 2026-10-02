namespace AppAny.Quartz.EntityFrameworkCore.Migrations
{
  public class QuartzMisfireHistory
  {
    public string SchedulerName { get; set; } = null!;
    public string EntryId { get; set; } = null!;
    public string InstanceName { get; set; } = null!;
    public string TriggerName { get; set; } = null!;
    public string TriggerGroup { get; set; } = null!;
    public string? JobName { get; set; }
    public string? JobGroup { get; set; }
    public long MisfireTime { get; set; }
    public long? ScheduledTime { get; set; }
    public int? Reason { get; set; }
  }
}