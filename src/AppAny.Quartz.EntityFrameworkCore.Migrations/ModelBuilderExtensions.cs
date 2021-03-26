using System;
using Microsoft.EntityFrameworkCore;

namespace AppAny.Quartz.EntityFrameworkCore.Migrations.PostgreSQL
{
  public static class ModelBuilderExtensions
  {
    public static ModelBuilder AddQuartz(
      this ModelBuilder modelBuilder,
      Action<QuartzModelBuilder>? configure)
    {
      var model = new QuartzModel();
      configure?.Invoke(new DefaultQuartzModelBuilder(model));

      if (model.EntityTypeConfigurations is null)
      {
        throw new InvalidOperationException("No database provider");
      }

      model.EntityTypeConfigurations.Invoke(
        new EntityTypeConfigurationContext(model.Prefix, model.Schema, modelBuilder));

      return modelBuilder;
    }
  }
}
