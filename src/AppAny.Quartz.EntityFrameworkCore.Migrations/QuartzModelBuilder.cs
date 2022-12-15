using System;

namespace AppAny.Quartz.EntityFrameworkCore.Migrations
{
  public interface QuartzModelBuilder
  {
    QuartzModelBuilder UseEntityTypeConfigurations(Action<EntityTypeConfigurationContext> entityTypeConfigurations);
  }

  public class DefaultQuartzModelBuilder : QuartzModelBuilder
  {
    private readonly QuartzModel model;

    public DefaultQuartzModelBuilder(QuartzModel model)
    {
      this.model = model;
    }

    public QuartzModelBuilder UseEntityTypeConfigurations(
      Action<EntityTypeConfigurationContext> entityTypeConfigurations)
    {
      model.EntityTypeConfigurations = entityTypeConfigurations;

      return this;
    }
  }
}
