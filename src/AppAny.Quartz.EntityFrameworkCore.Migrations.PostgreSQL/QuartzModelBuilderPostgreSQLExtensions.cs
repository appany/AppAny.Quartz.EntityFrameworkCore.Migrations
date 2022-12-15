namespace AppAny.Quartz.EntityFrameworkCore.Migrations.PostgreSQL
{
  public static class QuartzModelBuilderPostgreSqlExtensions
  {
    public const string DefaultPrefix = "qrtz_";

    public static QuartzModelBuilder UsePostgreSql(this QuartzModelBuilder builder)
    {
      builder.UsePrefix(DefaultPrefix).UseEntityTypeConfigurations(context =>
      {
        context.ModelBuilder.ApplyConfiguration(
          new QuartzJobDetailEntityTypeConfiguration(context.Prefix, context.Schema));

        context.ModelBuilder.ApplyConfiguration(
          new QuartzTriggerEntityTypeConfiguration(context.Prefix, context.Schema));

        context.ModelBuilder.ApplyConfiguration(
          new QuartzSimpleTriggerEntityTypeConfiguration(context.Prefix, context.Schema));

        context.ModelBuilder.ApplyConfiguration(
          new QuartzSimplePropertyTriggerEntityTypeConfiguration(context.Prefix, context.Schema));

        context.ModelBuilder.ApplyConfiguration(
          new QuartzCronTriggerEntityTypeConfiguration(context.Prefix, context.Schema));

        context.ModelBuilder.ApplyConfiguration(
          new QuartzBlobTriggerEntityTypeConfiguration(context.Prefix, context.Schema));

        context.ModelBuilder.ApplyConfiguration(
          new QuartzCalendarEntityTypeConfiguration(context.Prefix, context.Schema));

        context.ModelBuilder.ApplyConfiguration(
          new QuartzPausedTriggerGroupEntityTypeConfiguration(context.Prefix, context.Schema));

        context.ModelBuilder.ApplyConfiguration(
          new QuartzFiredTriggerEntityTypeConfiguration(context.Prefix, context.Schema));

        context.ModelBuilder.ApplyConfiguration(
          new QuartzSchedulerStateEntityTypeConfiguration(context.Prefix, context.Schema));

        context.ModelBuilder.ApplyConfiguration(
          new QuartzLockEntityTypeConfiguration(context.Prefix, context.Schema));
      });

      return builder;
    }
  }
}
