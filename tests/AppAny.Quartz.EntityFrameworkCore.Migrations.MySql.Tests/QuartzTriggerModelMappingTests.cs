namespace AppAny.Quartz.EntityFrameworkCore.Migrations.MySql.Tests;

using AppAny.Quartz.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

public class QuartzTriggerModelMappingTests
{
  [Fact]
  public void ShouldMapMisfireOriginalFireTimeColumn()
  {
    var options = new DbContextOptionsBuilder<MySqlIntegrationDbContext>()
      .UseMySql(
        "Server=localhost;Port=3306;Database=quartz_mapping_tests;User=root;Password=password",
        ServerVersion.Parse("8.0.36-mysql"))
      .Options;

    using var dbContext = new MySqlIntegrationDbContext(options);
    var entityType = dbContext.Model.FindEntityType(typeof(QuartzTrigger));

    Assert.NotNull(entityType);

    var property = entityType!.FindProperty(nameof(QuartzTrigger.MisfireOriginalFireTime));
    Assert.NotNull(property);

    var table = StoreObjectIdentifier.Table(entityType.GetTableName()!, entityType.GetSchema());
    Assert.Equal("MISFIRE_ORIG_FIRE_TIME", property!.GetColumnName(table));
    Assert.Equal("bigint(19)", property.GetColumnType());
  }
}
