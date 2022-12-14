namespace AppAny.Quartz.EntityFrameworkCore.Migrations
{
  public class QuartzBlobTrigger
  {
    public static readonly string TableName = "BLOB_TRIGGERS";

    public string SchedulerName { get; set; } = null!;
    public string TriggerName { get; set; } = null!;
    public string TriggerGroup { get; set; } = null!;
    public byte[]? BlobData { get; set; }

    public QuartzTrigger Trigger { get; set; } = null!;
  }
}
