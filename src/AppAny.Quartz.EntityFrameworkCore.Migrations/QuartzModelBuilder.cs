using System;

namespace AppAny.Quartz.EntityFrameworkCore.Migrations.PostgreSQL
{
	public interface QuartzModelBuilder
	{
		QuartzModelBuilder UsePrefix(string? prefix);
		QuartzModelBuilder UseSchema(string schema);

		QuartzModelBuilder UseEntityTypeConfigurations(
			Action<EntityTypeConfigurationContext> entityTypeConfigurations);
	}

	public class DefaultQuartzModelBuilder : QuartzModelBuilder
	{
		private readonly QuartzModel options;

		public DefaultQuartzModelBuilder(QuartzModel options)
		{
			this.options = options;
		}

		public QuartzModelBuilder UsePrefix(string? prefix)
		{
			options.Prefix = prefix;

			return this;
		}

		public QuartzModelBuilder UseSchema(string schema)
		{
			options.Schema = schema;

			return this;
		}

		public QuartzModelBuilder UseEntityTypeConfigurations(
			Action<EntityTypeConfigurationContext> entityTypeConfigurations)
		{
			options.EntityTypeConfigurations = entityTypeConfigurations;

			return this;
		}
	}
}
