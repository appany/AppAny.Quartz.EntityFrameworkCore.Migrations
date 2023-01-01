namespace AppAny.Quartz.EntityFrameworkCore.Migrations.SQLite;

public static class QuartzModelBuilderSQLiteExtensions
{
  /// <summary>
  /// Setup the use of Sqlite tables for Quartz.NET.
  /// <remarks>SQLite does not support schemas which is why no schema option can be set as this will be ignored by the SQLite provider.</remarks>
  /// </summary>
  /// <param name="builder">Fluent API to use Sqlite tables for Quartz.NET.</param>
  /// <param name="prefix">Optional prefix for every table created.</param>
  /// <returns></returns>
  public static QuartzModelBuilder UseSqlite(this QuartzModelBuilder builder, string prefix = "QRTZ_")
  {
    builder.UseEntityTypeConfigurations(context =>
    {
      context.ModelBuilder.ApplyConfiguration(
        new QuartzJobDetailEntityTypeConfiguration(prefix));

      context.ModelBuilder.ApplyConfiguration(
        new QuartzTriggerEntityTypeConfiguration(prefix));

      context.ModelBuilder.ApplyConfiguration(
        new QuartzSimpleTriggerEntityTypeConfiguration(prefix));

      context.ModelBuilder.ApplyConfiguration(
        new QuartzSimplePropertyTriggerEntityTypeConfiguration(prefix));

      context.ModelBuilder.ApplyConfiguration(
        new QuartzCronTriggerEntityTypeConfiguration(prefix));

      context.ModelBuilder.ApplyConfiguration(
        new QuartzBlobTriggerEntityTypeConfiguration(prefix));

      context.ModelBuilder.ApplyConfiguration(
        new QuartzCalendarEntityTypeConfiguration(prefix));

      context.ModelBuilder.ApplyConfiguration(
        new QuartzPausedTriggerGroupEntityTypeConfiguration(prefix));

      context.ModelBuilder.ApplyConfiguration(
        new QuartzFiredTriggerEntityTypeConfiguration(prefix));

      context.ModelBuilder.ApplyConfiguration(
        new QuartzSchedulerStateEntityTypeConfiguration(prefix));

      context.ModelBuilder.ApplyConfiguration(
        new QuartzLockEntityTypeConfiguration(prefix));
    });

    return builder;
  }
}
