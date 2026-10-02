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
    using var dbContext = new SQLiteIntegrationDbContext(CreateOptions());
    var entityType = dbContext.Model.FindEntityType(typeof(QuartzTrigger));

    Assert.NotNull(entityType);

    var property = entityType!.FindProperty(nameof(QuartzTrigger.MisfireOriginalFireTime));
    Assert.NotNull(property);

    var table = StoreObjectIdentifier.Table(entityType.GetTableName()!, entityType.GetSchema());
    Assert.Equal("MISFIRE_ORIG_FIRE_TIME", property!.GetColumnName(table));
    Assert.Equal("bigint", property.GetColumnType());
  }

  [Fact]
  public void ShouldMapExecutionGroupColumnOnTriggers()
  {
    using var dbContext = new SQLiteIntegrationDbContext(CreateOptions());
    var entityType = dbContext.Model.FindEntityType(typeof(QuartzTrigger));

    Assert.NotNull(entityType);

    var property = entityType!.FindProperty(nameof(QuartzTrigger.ExecutionGroup));
    Assert.NotNull(property);

    var table = StoreObjectIdentifier.Table(entityType.GetTableName()!, entityType.GetSchema());
    Assert.Equal("EXECUTION_GROUP", property!.GetColumnName(table));
    Assert.Equal("text", property.GetColumnType());
    Assert.True(property.IsNullable);
  }

  [Fact]
  public void ShouldMapExecutionGroupColumnOnFiredTriggers()
  {
    using var dbContext = new SQLiteIntegrationDbContext(CreateOptions());
    var entityType = dbContext.Model.FindEntityType(typeof(QuartzFiredTrigger));

    Assert.NotNull(entityType);

    var property = entityType!.FindProperty(nameof(QuartzFiredTrigger.ExecutionGroup));
    Assert.NotNull(property);

    var table = StoreObjectIdentifier.Table(entityType.GetTableName()!, entityType.GetSchema());
    Assert.Equal("EXECUTION_GROUP", property!.GetColumnName(table));
    Assert.Equal("text", property.GetColumnType());
    Assert.True(property.IsNullable);
  }

  [Fact]
  public void ShouldMapPreferredNodeColumnsOnTriggers()
  {
    using var dbContext = new SQLiteIntegrationDbContext(CreateOptions());
    var entityType = dbContext.Model.FindEntityType(typeof(QuartzTrigger));

    Assert.NotNull(entityType);

    var table = StoreObjectIdentifier.Table(entityType!.GetTableName()!, entityType.GetSchema());

    var preferredNode = entityType.FindProperty(nameof(QuartzTrigger.PreferredNode));
    Assert.NotNull(preferredNode);
    Assert.Equal("PREFERRED_NODE", preferredNode!.GetColumnName(table));
    Assert.Equal("text", preferredNode.GetColumnType());
    Assert.True(preferredNode.IsNullable);

    var preferredNodeAuto = entityType.FindProperty(nameof(QuartzTrigger.PreferredNodeAuto));
    Assert.NotNull(preferredNodeAuto);
    Assert.Equal("PREFERRED_NODE_AUTO", preferredNodeAuto!.GetColumnName(table));
    Assert.Equal("bit", preferredNodeAuto.GetColumnType());
    Assert.False(preferredNodeAuto.IsNullable);
    Assert.Equal(false, preferredNodeAuto.GetDefaultValue());
  }

  private static DbContextOptions<SQLiteIntegrationDbContext> CreateOptions()
  {
    var connectionString = new SqliteConnectionStringBuilder
    {
      Mode = SqliteOpenMode.Memory,
      DataSource = ":memory:"
    }.ToString();

    return new DbContextOptionsBuilder<SQLiteIntegrationDbContext>()
      .UseSqlite(connectionString)
      .Options;
  }
}
