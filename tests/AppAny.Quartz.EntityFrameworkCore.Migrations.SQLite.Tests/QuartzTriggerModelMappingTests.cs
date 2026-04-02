namespace AppAny.Quartz.EntityFrameworkCore.Migrations.SQLite.Tests;

using AppAny.Quartz.EntityFrameworkCore.Migrations;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

public class QuartzTriggerModelMappingTests
{
  [Fact]
  public void ShouldMapMisfireOriginalFireTimeColumn()
  {
    var connectionString = new SqliteConnectionStringBuilder
    {
      Mode = SqliteOpenMode.Memory,
      DataSource = ":memory:"
    }.ToString();

    var options = new DbContextOptionsBuilder<SQLiteIntegrationDbContext>()
      .UseSqlite(connectionString)
      .Options;

    using var dbContext = new SQLiteIntegrationDbContext(options);
    var entityType = dbContext.Model.FindEntityType(typeof(QuartzTrigger));

    Assert.NotNull(entityType);

    var property = entityType!.FindProperty(nameof(QuartzTrigger.MisfireOriginalFireTime));
    Assert.NotNull(property);

    var table = StoreObjectIdentifier.Table(entityType.GetTableName()!, entityType.GetSchema());
    Assert.Equal("MISFIRE_ORIG_FIRE_TIME", property!.GetColumnName(table));
    Assert.Equal("bigint", property.GetColumnType());
  }
}
