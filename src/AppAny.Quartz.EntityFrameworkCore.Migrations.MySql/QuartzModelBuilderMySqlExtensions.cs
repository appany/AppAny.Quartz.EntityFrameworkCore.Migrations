namespace AppAny.Quartz.EntityFrameworkCore.Migrations.MySql
{
  public static class QuartzModelBuilderMySqlExtensions
  {
    public const string DefaultPrefix = "QRTZ_";

    public static QuartzModelBuilder UseMySql(this QuartzModelBuilder builder)
    {
      builder.UsePrefix(DefaultPrefix).UseEntityTypeConfigurations(context =>
      {
        context.ModelBuilder.ApplyConfiguration(
          new QuartzJobDetailEntityTypeConfiguration(context.Prefix));

        context.ModelBuilder.ApplyConfiguration(
          new QuartzTriggerEntityTypeConfiguration(context.Prefix));

        context.ModelBuilder.ApplyConfiguration(
          new QuartzSimpleTriggerEntityTypeConfiguration(context.Prefix));

        context.ModelBuilder.ApplyConfiguration(
          new QuartzSimplePropertyTriggerEntityTypeConfiguration(context.Prefix));

        context.ModelBuilder.ApplyConfiguration(
          new QuartzCronTriggerEntityTypeConfiguration(context.Prefix));

        context.ModelBuilder.ApplyConfiguration(
          new QuartzBlobTriggerEntityTypeConfiguration(context.Prefix));

        context.ModelBuilder.ApplyConfiguration(
          new QuartzCalendarEntityTypeConfiguration(context.Prefix));

        context.ModelBuilder.ApplyConfiguration(
          new QuartzPausedTriggerGroupEntityTypeConfiguration(context.Prefix));

        context.ModelBuilder.ApplyConfiguration(
          new QuartzFiredTriggerEntityTypeConfiguration(context.Prefix));

        context.ModelBuilder.ApplyConfiguration(
          new QuartzSchedulerStateEntityTypeConfiguration(context.Prefix));

        context.ModelBuilder.ApplyConfiguration(
          new QuartzLockEntityTypeConfiguration(context.Prefix));
      });

      return builder;
    }
  }
}
