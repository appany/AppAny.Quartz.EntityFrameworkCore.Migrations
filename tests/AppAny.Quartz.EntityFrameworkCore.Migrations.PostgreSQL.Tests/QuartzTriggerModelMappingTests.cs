namespace AppAny.Quartz.EntityFrameworkCore.Migrations.PostgreSQL.Tests;

using AppAny.Quartz.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

public class QuartzTriggerModelMappingTests
{
  [Fact]
  public void ShouldMapMisfireOriginalFireTimeColumn()
  {
    var options = new DbContextOptionsBuilder<PostgreSqlIntegrationDbContext>()
      .UseNpgsql("Host=localhost;Port=5432;Database=quartz_mapping_tests;Username=postgres;Password=postgres")
      .Options;

    using var dbContext = new PostgreSqlIntegrationDbContext(options);
    var entityType = dbContext.Model.FindEntityType(typeof(QuartzTrigger));

    Assert.NotNull(entityType);

    var property = entityType!.FindProperty(nameof(QuartzTrigger.MisfireOriginalFireTime));
    Assert.NotNull(property);

    var table = StoreObjectIdentifier.Table(entityType.GetTableName()!, entityType.GetSchema());
    Assert.Equal("misfire_orig_fire_time", property!.GetColumnName(table));
    Assert.Equal("bigint", property.GetColumnType());
  }
}
