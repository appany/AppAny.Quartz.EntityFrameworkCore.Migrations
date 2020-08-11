using System;
using Microsoft.EntityFrameworkCore;

namespace AppAny.Quartz.EntityFrameworkCore.Migrations.PostgreSQL
{
	public static class QuartzModelBuilderExtensions
	{
		public static ModelBuilder AddQuartzPostgres(
			this ModelBuilder modelBuilder,
			Action<IQuartzModelOptionsBuilder>? builder = null)
		{
			var options = new QuartzModelOptions();
			builder?.Invoke(new QuartzModelOptionsBuilder(options));

			modelBuilder.ApplyConfiguration(
				new QuartzJobDetailEntityTypeConfiguration(options.Prefix, options.Schema));

			modelBuilder.ApplyConfiguration(
				new QuartzTriggerEntityTypeConfiguration(options.Prefix, options.Schema));

			modelBuilder.ApplyConfiguration(
				new QuartzSimpleTriggerEntityTypeConfiguration(options.Prefix, options.Schema));

			modelBuilder.ApplyConfiguration(
				new QuartzSimplePropertyTriggerEntityTypeConfiguration(options.Prefix, options.Schema));

			modelBuilder.ApplyConfiguration(
				new QuartzCronTriggerEntityTypeConfiguration(options.Prefix, options.Schema));

			modelBuilder.ApplyConfiguration(
				new QuartzBlobTriggerEntityTypeConfiguration(options.Prefix, options.Schema));

			modelBuilder.ApplyConfiguration(
				new QuartzCalendarEntityTypeConfiguration(options.Prefix, options.Schema));

			modelBuilder.ApplyConfiguration(
				new QuartzPausedTriggerGroupEntityTypeConfiguration(options.Prefix, options.Schema));

			modelBuilder.ApplyConfiguration(
				new QuartzFiredTriggerEntityTypeConfiguration(options.Prefix, options.Schema));

			modelBuilder.ApplyConfiguration(
				new QuartzSchedulerStateEntityTypeConfiguration(options.Prefix, options.Schema));

			modelBuilder.ApplyConfiguration(
				new QuartzLockEntityTypeConfiguration(options.Prefix, options.Schema));

			return modelBuilder;
		}
	}
}