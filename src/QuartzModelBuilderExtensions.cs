using Microsoft.EntityFrameworkCore;

namespace AppAny.Quartz.EntityFrameworkCore.Migrations.PostgreSQL
{
	public static class QuartzModelBuilderExtensions
	{
		public static ModelBuilder AddQuartzPostgreSQL(
			this ModelBuilder modelBuilder,
			string prefix = "qrtz_",
			string? schema = null)
		{
			modelBuilder.ApplyConfiguration(new QuartzJobDetailEntityTypeConfiguration(prefix, schema));
			modelBuilder.ApplyConfiguration(new QuartzTriggerEntityTypeConfiguration(prefix, schema));
			modelBuilder.ApplyConfiguration(new QuartzSimpleTriggerEntityTypeConfiguration(prefix, schema));
			modelBuilder.ApplyConfiguration(new QuartzSimplePropertyTriggerEntityTypeConfiguration(prefix, schema));
			modelBuilder.ApplyConfiguration(new QuartzCronTriggerEntityTypeConfiguration(prefix, schema));
			modelBuilder.ApplyConfiguration(new QuartzBlobTriggerEntityTypeConfiguration(prefix, schema));
			modelBuilder.ApplyConfiguration(new QuartzCalendarEntityTypeConfiguration(prefix, schema));
			modelBuilder.ApplyConfiguration(new QuartzPausedTriggerGroupEntityTypeConfiguration(prefix, schema));
			modelBuilder.ApplyConfiguration(new QuartzFiredTriggerEntityTypeConfiguration(prefix, schema));
			modelBuilder.ApplyConfiguration(new QuartzSchedulerStateEntityTypeConfiguration(prefix, schema));
			modelBuilder.ApplyConfiguration(new QuartzLockEntityTypeConfiguration(prefix, schema));

			return modelBuilder;
		}
	}
}