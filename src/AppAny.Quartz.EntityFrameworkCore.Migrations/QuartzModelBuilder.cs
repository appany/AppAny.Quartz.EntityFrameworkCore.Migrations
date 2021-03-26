using System;

namespace AppAny.Quartz.EntityFrameworkCore.Migrations.PostgreSQL
{
  public interface QuartzModelBuilder
  {
    QuartzModelBuilder UsePrefix(string? prefix);
    QuartzModelBuilder UseSchema(string schema);

    QuartzModelBuilder UseEntityTypeConfigurations(Action<EntityTypeConfigurationContext> entityTypeConfigurations);
  }

  public class DefaultQuartzModelBuilder : QuartzModelBuilder
  {
    private readonly QuartzModel model;

    public DefaultQuartzModelBuilder(QuartzModel model)
    {
      this.model = model;
    }

    public QuartzModelBuilder UsePrefix(string? prefix)
    {
      model.Prefix = prefix;

      return this;
    }

    public QuartzModelBuilder UseSchema(string schema)
    {
      model.Schema = schema;

      return this;
    }

    public QuartzModelBuilder UseEntityTypeConfigurations(
      Action<EntityTypeConfigurationContext> entityTypeConfigurations)
    {
      model.EntityTypeConfigurations = entityTypeConfigurations;

      return this;
    }
  }
}
