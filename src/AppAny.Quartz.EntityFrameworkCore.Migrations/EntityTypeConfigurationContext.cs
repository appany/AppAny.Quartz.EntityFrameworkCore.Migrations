using Microsoft.EntityFrameworkCore;

namespace AppAny.Quartz.EntityFrameworkCore.Migrations
{
  public readonly struct EntityTypeConfigurationContext
  {
    public EntityTypeConfigurationContext(ModelBuilder modelBuilder)
    {
      ModelBuilder = modelBuilder;
    }

    public ModelBuilder ModelBuilder { get; }
  }
}
