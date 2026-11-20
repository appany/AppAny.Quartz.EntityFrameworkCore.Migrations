namespace AppAny.Quartz.EntityFrameworkCore.Migrations
{
  public class QuartzJobStatus
  {
    public string SchedulerName { get; set; } = null!;
    public string JobGroup { get; set; } = null!;
    public string JobName { get; set; } = null!;
    public long FirstFiredTime { get; set; }
    public long LastFiredTime { get; set; }
    public int LastResult { get; set; }
    public long LastRunTime { get; set; }
    public string LastInstanceName { get; set; } = null!;
    public string? LastEntryId { get; set; }
    public string? LastSummary { get; set; }
    public long? LastSuccessTime { get; set; }
    public long? LastFailureTime { get; set; }
    public string? LastFailureMessage { get; set; }
    public int ConsecutiveFailures { get; set; }
    public long RunCount { get; set; }
    public long FailureCount { get; set; }
  }
}