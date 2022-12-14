namespace AppAny.Quartz.EntityFrameworkCore.Migrations
{
  public class QuartzSchedulerState
  {
    public static readonly string TableName = "SCHEDULER_STATE";

    public string SchedulerName { get; set; } = null!;
    public string InstanceName { get; set; } = null!;
    public long LastCheckInTime { get; set; }
    public long CheckInInterval { get; set; }
  }
}
