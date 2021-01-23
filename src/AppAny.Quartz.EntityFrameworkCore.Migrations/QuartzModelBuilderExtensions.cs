using System;
using Microsoft.EntityFrameworkCore;

namespace AppAny.Quartz.EntityFrameworkCore.Migrations.PostgreSQL
{
	public static class QuartzModelBuilderExtensions
	{
		public static ModelBuilder AddQuartz(
			this ModelBuilder modelBuilder,
			Action<QuartzModelBuilder>? configure)
		{
			var options = new QuartzModel();
			configure?.Invoke(new DefaultQuartzModelBuilder(options));

			if (options.EntityTypeConfigurations is null)
			{
				throw new InvalidOperationException("No database provider");
			}

			options.EntityTypeConfigurations.Invoke(
				new EntityTypeConfigurationContext(options.Prefix, options.Schema, modelBuilder));

			return modelBuilder;
		}
	}
}
