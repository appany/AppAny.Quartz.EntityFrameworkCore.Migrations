using Microsoft.EntityFrameworkCore;

namespace AppAny.Quartz.EntityFrameworkCore.Migrations.PostgreSQL
{
	public static class QuartzModelBuilderExtensions
	{
		public static ModelBuilder AddQuartzPostgreSQL(this ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfiguration(new QuartzJobDetailEntityTypeConfiguration());
			modelBuilder.ApplyConfiguration(new QuartzTriggerEntityTypeConfiguration());
			modelBuilder.ApplyConfiguration(new QuartzSimpleTriggerEntityTypeConfiguration());
			modelBuilder.ApplyConfiguration(new QuartzSimplePropertyTriggerEntityTypeConfiguration());
			modelBuilder.ApplyConfiguration(new QuartzCronTriggerEntityTypeConfiguration());
			modelBuilder.ApplyConfiguration(new QuartzBlobTriggerEntityTypeConfiguration());
			modelBuilder.ApplyConfiguration(new QuartzCalendarEntityTypeConfiguration());
			modelBuilder.ApplyConfiguration(new QuartzPausedTriggerGroupEntityTypeConfiguration());
			modelBuilder.ApplyConfiguration(new QuartzFiredTriggerEntityTypeConfiguration());
			modelBuilder.ApplyConfiguration(new QuartzSchedulerStateEntityTypeConfiguration());
			modelBuilder.ApplyConfiguration(new QuartzLockEntityTypeConfiguration());

			return modelBuilder;
		}
	}
}