namespace AppAny.Quartz.EntityFrameworkCore.Migrations.PostgreSQL
{
  public static class QuartzModelBuilderPostgreSqlExtensions
  {
    public static QuartzModelBuilder UsePostgreSql(this QuartzModelBuilder builder, string prefix = "qrtz_", string? schema = "quartz")
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
