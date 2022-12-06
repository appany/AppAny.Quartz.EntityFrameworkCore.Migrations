using Microsoft.EntityFrameworkCore;

namespace AppAny.Quartz.EntityFrameworkCore.Migrations
{
  public readonly struct EntityTypeConfigurationContext
  {
    public EntityTypeConfigurationContext(string? prefix, string? schema, ModelBuilder modelBuilder)
    {
      Prefix = prefix;
      Schema = schema;
      ModelBuilder = modelBuilder;
    }

    public string? Prefix { get; }
    public string? Schema { get; }
    public ModelBuilder ModelBuilder { get; }
  }
}
