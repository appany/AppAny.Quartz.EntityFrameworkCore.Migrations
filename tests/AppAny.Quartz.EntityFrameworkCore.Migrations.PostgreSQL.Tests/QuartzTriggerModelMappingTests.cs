namespace AppAny.Quartz.EntityFrameworkCore.Migrations.PostgreSQL.Tests;

using AppAny.Quartz.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

public class QuartzTriggerModelMappingTests
{
  [Fact]
  public void ShouldMapMisfireOriginalFireTimeColumn()
  {
    using var dbContext = new PostgreSqlIntegrationDbContext(CreateOptions());
    var entityType = dbContext.Model.FindEntityType(typeof(QuartzTrigger));

    Assert.NotNull(entityType);

    var property = entityType!.FindProperty(nameof(QuartzTrigger.MisfireOriginalFireTime));
    Assert.NotNull(property);

    var table = StoreObjectIdentifier.Table(entityType.GetTableName()!, entityType.GetSchema());
    Assert.Equal("misfire_orig_fire_time", property!.GetColumnName(table));
    Assert.Equal("bigint", property.GetColumnType());
  }

  [Fact]
  public void ShouldMapExecutionGroupColumnOnTriggers()
  {
    using var dbContext = new PostgreSqlIntegrationDbContext(CreateOptions());
    var entityType = dbContext.Model.FindEntityType(typeof(QuartzTrigger));

    Assert.NotNull(entityType);

    var property = entityType!.FindProperty(nameof(QuartzTrigger.ExecutionGroup));
    Assert.NotNull(property);

    var table = StoreObjectIdentifier.Table(entityType.GetTableName()!, entityType.GetSchema());
    Assert.Equal("execution_group", property!.GetColumnName(table));
    Assert.Equal("varchar(200)", property.GetColumnType());
    Assert.True(property.IsNullable);
  }

  [Fact]
  public void ShouldMapExecutionGroupColumnOnFiredTriggers()
  {
    using var dbContext = new PostgreSqlIntegrationDbContext(CreateOptions());
    var entityType = dbContext.Model.FindEntityType(typeof(QuartzFiredTrigger));

    Assert.NotNull(entityType);

    var property = entityType!.FindProperty(nameof(QuartzFiredTrigger.ExecutionGroup));
    Assert.NotNull(property);

    var table = StoreObjectIdentifier.Table(entityType.GetTableName()!, entityType.GetSchema());
    Assert.Equal("execution_group", property!.GetColumnName(table));
    Assert.Equal("varchar(200)", property.GetColumnType());
    Assert.True(property.IsNullable);
  }

  [Fact]
  public void ShouldMapPreferredNodeColumnsOnTriggers()
  {
    using var dbContext = new PostgreSqlIntegrationDbContext(CreateOptions());
    var entityType = dbContext.Model.FindEntityType(typeof(QuartzTrigger));

    Assert.NotNull(entityType);

    var table = StoreObjectIdentifier.Table(entityType!.GetTableName()!, entityType.GetSchema());

    var preferredNode = entityType.FindProperty(nameof(QuartzTrigger.PreferredNode));
    Assert.NotNull(preferredNode);
    Assert.Equal("preferred_node", preferredNode!.GetColumnName(table));
    Assert.Equal("varchar(200)", preferredNode.GetColumnType());
    Assert.True(preferredNode.IsNullable);

    var preferredNodeAuto = entityType.FindProperty(nameof(QuartzTrigger.PreferredNodeAuto));
    Assert.NotNull(preferredNodeAuto);
    Assert.Equal("preferred_node_auto", preferredNodeAuto!.GetColumnName(table));
    Assert.Equal("boolean", preferredNodeAuto.GetColumnType());
    Assert.False(preferredNodeAuto.IsNullable);
    Assert.Equal(false, preferredNodeAuto.GetDefaultValue());
  }

  private static DbContextOptions<PostgreSqlIntegrationDbContext> CreateOptions()
  {
    return new DbContextOptionsBuilder<PostgreSqlIntegrationDbContext>()
      .UseNpgsql("Host=localhost;Port=5432;Database=quartz_mapping_tests;Username=postgres;Password=postgres")
      .Options;
  }
}
