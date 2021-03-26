using System;

namespace AppAny.Quartz.EntityFrameworkCore.Migrations.PostgreSQL
{
  public class QuartzModel
  {
    public string? Prefix { get; set; } = "qrtz_";
    public string? Schema { get; set; }
    public Action<EntityTypeConfigurationContext>? EntityTypeConfigurations { get; set; }
  }
}
