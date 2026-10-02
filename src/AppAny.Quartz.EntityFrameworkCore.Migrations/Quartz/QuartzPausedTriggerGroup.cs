namespace AppAny.Quartz.EntityFrameworkCore.Migrations
{
  public class QuartzPausedTriggerGroup
  {
    public string SchedulerName { get; set; } = null!;
    public string TriggerGroup { get; set; } = null!;
    public string? PauseReason { get; set; }
    public string? PausedBy { get; set; }
    public long? PausedAt { get; set; }
  }
}
