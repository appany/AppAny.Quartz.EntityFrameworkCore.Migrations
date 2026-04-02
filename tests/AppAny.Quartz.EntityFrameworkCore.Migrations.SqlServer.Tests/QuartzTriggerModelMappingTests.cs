namespace AppAny.Quartz.EntityFrameworkCore.Migrations.SqlServer.Tests;

using AppAny.Quartz.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

public class QuartzTriggerModelMappingTests
{
  [Fact]
  public void ShouldMapMisfireOriginalFireTimeColumn()
  {
    var options = new DbContextOptionsBuilder<SqlServerIntegrationDbContext>()
      .UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=QuartzMappingTests;Trusted_Connection=True;")
      .Options;

    using var dbContext = new SqlServerIntegrationDbContext(options);
    var entityType = dbContext.Model.FindEntityType(typeof(QuartzTrigger));

    Assert.NotNull(entityType);

    var property = entityType!.FindProperty(nameof(QuartzTrigger.MisfireOriginalFireTime));
    Assert.NotNull(property);

    var table = StoreObjectIdentifier.Table(entityType.GetTableName()!, entityType.GetSchema());
    Assert.Equal("MISFIRE_ORIG_FIRE_TIME", property!.GetColumnName(table));
    Assert.Equal("bigint", property.GetColumnType());
  }
}
