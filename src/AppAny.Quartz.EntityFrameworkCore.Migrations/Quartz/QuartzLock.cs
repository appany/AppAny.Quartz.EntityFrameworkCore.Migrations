namespace AppAny.Quartz.EntityFrameworkCore.Migrations.PostgreSQL
{
  public class QuartzLock
  {
    public string SchedulerName { get; set; } = null!;
    public string LockName { get; set; } = null!;
  }
}
