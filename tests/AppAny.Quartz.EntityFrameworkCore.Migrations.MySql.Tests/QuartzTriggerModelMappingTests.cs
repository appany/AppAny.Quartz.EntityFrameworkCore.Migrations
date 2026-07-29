namespace AppAny.Quartz.EntityFrameworkCore.Migrations.MySql.Tests;

using AppAny.Quartz.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

public class QuartzTriggerModelMappingTests
{
  [Fact]
  public void ShouldMapMisfireOriginalFireTimeColumn()
  {
#if NET10_0_OR_GREATER
    // Oracle MySQL provider uses UseMySQL (capital SQL) and doesn't require ServerVersion parameter
    var options = new DbContextOptionsBuilder<MySqlIntegrationDbContext>()
      .UseMySQL("Server=localhost;Port=3306;Database=quartz_mapping_tests;User=root;Password=password")
      .Options;
#else
    // Pomelo provider uses UseMySql and requires ServerVersion parameter (NET8_0)
    var options = new DbContextOptionsBuilder<MySqlIntegrationDbContext>()
      .UseMySql(
        "Server=localhost;Port=3306;Database=quartz_mapping_tests;User=root;Password=password",
        ServerVersion.Parse("8.0.36-mysql"))
      .Options;
#endif

    using var dbContext = new MySqlIntegrationDbContext(options);
    var entityType = dbContext.Model.FindEntityType(typeof(QuartzTrigger));

    Assert.NotNull(entityType);

    var property = entityType!.FindProperty(nameof(QuartzTrigger.MisfireOriginalFireTime));
    Assert.NotNull(property);

    var table = StoreObjectIdentifier.Table(entityType.GetTableName()!, entityType.GetSchema());
    Assert.Equal("MISFIRE_ORIG_FIRE_TIME", property!.GetColumnName(table));
    Assert.Equal("bigint(19)", property.GetColumnType());
  }

  [Fact]
  public void ShouldMapExecutionGroupColumnOnTriggers()
  {
    using var dbContext = new MySqlIntegrationDbContext(CreateOptions());
    var entityType = dbContext.Model.FindEntityType(typeof(QuartzTrigger));

    Assert.NotNull(entityType);

    var property = entityType!.FindProperty(nameof(QuartzTrigger.ExecutionGroup));
    Assert.NotNull(property);

    var table = StoreObjectIdentifier.Table(entityType.GetTableName()!, entityType.GetSchema());
    Assert.Equal("EXECUTION_GROUP", property!.GetColumnName(table));
    Assert.Equal("varchar(200)", property.GetColumnType());
    Assert.True(property.IsNullable);
  }

  [Fact]
  public void ShouldMapExecutionGroupColumnOnFiredTriggers()
  {
    using var dbContext = new MySqlIntegrationDbContext(CreateOptions());
    var entityType = dbContext.Model.FindEntityType(typeof(QuartzFiredTrigger));

    Assert.NotNull(entityType);

    var property = entityType!.FindProperty(nameof(QuartzFiredTrigger.ExecutionGroup));
    Assert.NotNull(property);

    var table = StoreObjectIdentifier.Table(entityType.GetTableName()!, entityType.GetSchema());
    Assert.Equal("EXECUTION_GROUP", property!.GetColumnName(table));
    Assert.Equal("varchar(200)", property.GetColumnType());
    Assert.True(property.IsNullable);
  }

  [Fact]
  public void ShouldMapPreferredNodeColumnsOnTriggers()
  {
    using var dbContext = new MySqlIntegrationDbContext(CreateOptions());
    var entityType = dbContext.Model.FindEntityType(typeof(QuartzTrigger));

    Assert.NotNull(entityType);

    var table = StoreObjectIdentifier.Table(entityType!.GetTableName()!, entityType.GetSchema());

    var preferredNode = entityType.FindProperty(nameof(QuartzTrigger.PreferredNode));
    Assert.NotNull(preferredNode);
    Assert.Equal("PREFERRED_NODE", preferredNode!.GetColumnName(table));
    Assert.Equal("varchar(200)", preferredNode.GetColumnType());
    Assert.True(preferredNode.IsNullable);

    var preferredNodeAuto = entityType.FindProperty(nameof(QuartzTrigger.PreferredNodeAuto));
    Assert.NotNull(preferredNodeAuto);
    Assert.Equal("PREFERRED_NODE_AUTO", preferredNodeAuto!.GetColumnName(table));
    Assert.Equal("tinyint(1)", preferredNodeAuto.GetColumnType());
    Assert.False(preferredNodeAuto.IsNullable);
    Assert.Equal(false, preferredNodeAuto.GetDefaultValue());
  }

  private static DbContextOptions<MySqlIntegrationDbContext> CreateOptions()
  {
#if NET10_0_OR_GREATER
    // Oracle MySQL provider uses UseMySQL (capital SQL) and doesn't require ServerVersion parameter
    return new DbContextOptionsBuilder<MySqlIntegrationDbContext>()
      .UseMySQL("Server=localhost;Port=3306;Database=quartz_mapping_tests;User=root;Password=password")
      .Options;
#else
    // Pomelo provider uses UseMySql and requires ServerVersion parameter (NET8_0)
    return new DbContextOptionsBuilder<MySqlIntegrationDbContext>()
      .UseMySql(
        "Server=localhost;Port=3306;Database=quartz_mapping_tests;User=root;Password=password",
        ServerVersion.Parse("8.0.36-mysql"))
      .Options;
#endif
  }
}
