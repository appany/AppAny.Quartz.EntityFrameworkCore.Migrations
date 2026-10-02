namespace AppAny.Quartz.EntityFrameworkCore.Migrations
{
  public class QuartzExecutionHistory
  {
    public string SchedulerName { get; set; } = null!;
    public string EntryId { get; set; } = null!;
    public string InstanceName { get; set; } = null!;
    public string JobName { get; set; } = null!;
    public string JobGroup { get; set; } = null!;
    public string TriggerName { get; set; } = null!;
    public string TriggerGroup { get; set; } = null!;
    public long FiredTime { get; set; }
    public long RunTime { get; set; }
    public bool Succeeded { get; set; }
    public string? ErrorMessage { get; set; }
    public int RetryAttempt { get; set; }
    public bool RetryScheduled { get; set; }
    public string? ExecutionLog { get; set; }
  }
}