namespace AppAny.Quartz.EntityFrameworkCore.Migrations.SqlServer
{
  public static class QuartzModelBuilderSqlServerExtensions
  {
    public static QuartzModelBuilder UseSqlServer(this QuartzModelBuilder builder, string prefix = "QRTZ_", string schema = "")
    {
      builder.UseEntityTypeConfigurations(context =>
      {
        context.ModelBuilder.ApplyConfiguration(
          new QuartzJobDetailEntityTypeConfiguration(prefix, schema));

        context.ModelBuilder.ApplyConfiguration(
          new QuartzTriggerEntityTypeConfiguration(prefix, schema));

        context.ModelBuilder.ApplyConfiguration(
          new QuartzSimpleTriggerEntityTypeConfiguration(prefix, schema));

        context.ModelBuilder.ApplyConfiguration(
          new QuartzSimplePropertyTriggerEntityTypeConfiguration(prefix, schema));

        context.ModelBuilder.ApplyConfiguration(
          new QuartzCronTriggerEntityTypeConfiguration(prefix, schema));

        context.ModelBuilder.ApplyConfiguration(
          new QuartzBlobTriggerEntityTypeConfiguration(prefix, schema));

        context.ModelBuilder.ApplyConfiguration(
          new QuartzCalendarEntityTypeConfiguration(prefix, schema));

        context.ModelBuilder.ApplyConfiguration(
          new QuartzPausedTriggerGroupEntityTypeConfiguration(prefix, schema));

        context.ModelBuilder.ApplyConfiguration(
          new QuartzFiredTriggerEntityTypeConfiguration(prefix, schema));

        context.ModelBuilder.ApplyConfiguration(
          new QuartzSchedulerStateEntityTypeConfiguration(prefix, schema));

        context.ModelBuilder.ApplyConfiguration(
          new QuartzLockEntityTypeConfiguration(prefix, schema));
      });

      return builder;
    }
  }
}
