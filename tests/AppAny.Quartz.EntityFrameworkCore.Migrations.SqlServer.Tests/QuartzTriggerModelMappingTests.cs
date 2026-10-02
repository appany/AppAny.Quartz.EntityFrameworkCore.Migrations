namespace AppAny.Quartz.EntityFrameworkCore.Migrations.SqlServer.Tests;

using System;
using System.Linq;
using AppAny.Quartz.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;

public class QuartzTriggerModelMappingTests
{
  [Fact]
  public void ShouldMapMisfireOriginalFireTimeColumn()
  {
    using var dbContext = new SqlServerIntegrationDbContext(CreateOptions());
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
    using var dbContext = new SqlServerIntegrationDbContext(CreateOptions());
    var entityType = dbContext.Model.FindEntityType(typeof(QuartzTrigger));

    Assert.NotNull(entityType);

    var property = entityType!.FindProperty(nameof(QuartzTrigger.ExecutionGroup));
    Assert.NotNull(property);

    var table = StoreObjectIdentifier.Table(entityType.GetTableName()!, entityType.GetSchema());
    Assert.Equal("EXECUTION_GROUP", property!.GetColumnName(table));
    Assert.Equal(200, property.GetMaxLength());
    Assert.True(property.IsUnicode());
    Assert.True(property.IsNullable);
  }

  [Fact]
  public void ShouldMapExecutionGroupColumnOnFiredTriggers()
  {
    using var dbContext = new SqlServerIntegrationDbContext(CreateOptions());
    var entityType = dbContext.Model.FindEntityType(typeof(QuartzFiredTrigger));

    Assert.NotNull(entityType);

    var property = entityType!.FindProperty(nameof(QuartzFiredTrigger.ExecutionGroup));
    Assert.NotNull(property);

    var table = StoreObjectIdentifier.Table(entityType.GetTableName()!, entityType.GetSchema());
    Assert.Equal("EXECUTION_GROUP", property!.GetColumnName(table));
    Assert.Equal(200, property.GetMaxLength());
    Assert.True(property.IsUnicode());
    Assert.True(property.IsNullable);
  }

  [Fact]
  public void ShouldMapPreferredNodeColumnsOnTriggers()
  {
    using var dbContext = new SqlServerIntegrationDbContext(CreateOptions());
    var entityType = dbContext.Model.FindEntityType(typeof(QuartzTrigger));

    Assert.NotNull(entityType);

    var table = StoreObjectIdentifier.Table(entityType!.GetTableName()!, entityType.GetSchema());

    var preferredNode = entityType.FindProperty(nameof(QuartzTrigger.PreferredNode));
    Assert.NotNull(preferredNode);
    Assert.Equal("PREFERRED_NODE", preferredNode!.GetColumnName(table));
    Assert.Equal(200, preferredNode.GetMaxLength());
    Assert.True(preferredNode.IsUnicode());
    Assert.True(preferredNode.IsNullable);

    var preferredNodeAuto = entityType.FindProperty(nameof(QuartzTrigger.PreferredNodeAuto));
    Assert.NotNull(preferredNodeAuto);
    Assert.Equal("PREFERRED_NODE_AUTO", preferredNodeAuto!.GetColumnName(table));
    Assert.Equal("bit", preferredNodeAuto.GetColumnType());
    Assert.False(preferredNodeAuto.IsNullable);
    Assert.Equal(false, preferredNodeAuto.GetDefaultValue());
  }

  // Index set of the Quartz.NET 4.0.1 table scripts (database/tables/tables_sqlServer.sql)
  [Fact]
  public void ShouldMapQuartzIndexes()
  {
    using var dbContext = new SqlServerIntegrationDbContext(CreateOptions());

    Assert.Equal(
      new[] { "IDX_QRTZ_J_G_N (SCHED_NAME, JOB_GROUP, JOB_NAME)" },
      GetIndexes<QuartzJobDetail>(dbContext));

    Assert.Equal(
      new[]
      {
        "IDX_QRTZ_T_C (SCHED_NAME, CALENDAR_NAME)",
        "IDX_QRTZ_T_G_N (SCHED_NAME, TRIGGER_GROUP, TRIGGER_NAME)",
        "IDX_QRTZ_T_J (SCHED_NAME, JOB_NAME, JOB_GROUP)",
        "IDX_QRTZ_T_NFT_ST (SCHED_NAME, TRIGGER_STATE, NEXT_FIRE_TIME, PRIORITY DESC, MISFIRE_INSTR)"
      },
      GetIndexes<QuartzTrigger>(dbContext));

    Assert.Equal(
      new[]
      {
        "IDX_QRTZ_FT_INST_JOB_REQ_RCVRY (SCHED_NAME, INSTANCE_NAME, REQUESTS_RECOVERY)",
        "IDX_QRTZ_FT_J_G (SCHED_NAME, JOB_NAME, JOB_GROUP)",
        "IDX_QRTZ_FT_T_G (SCHED_NAME, TRIGGER_NAME, TRIGGER_GROUP)"
      },
      GetIndexes<QuartzFiredTrigger>(dbContext));
  }

  private static string[] GetIndexes<TEntity>(DbContext dbContext)
  {
    var entityType = dbContext.GetService<IDesignTimeModel>().Model.FindEntityType(typeof(TEntity))!;
    var table = StoreObjectIdentifier.Table(entityType.GetTableName()!, entityType.GetSchema());

    return entityType.GetIndexes()
      .Select(index =>
      {
        var columns = index.Properties.Select((property, i) =>
          index.IsDescending is { } descending && (descending.Count == 0 || descending[i])
            ? property.GetColumnName(table) + " DESC"
            : property.GetColumnName(table));

        return $"{index.GetDatabaseName()} ({string.Join(", ", columns)})";
      })
      .OrderBy(x => x, StringComparer.Ordinal)
      .ToArray();
  }

  private static DbContextOptions<SqlServerIntegrationDbContext> CreateOptions()
  {
    return new DbContextOptionsBuilder<SqlServerIntegrationDbContext>()
      .UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=QuartzMappingTests;Trusted_Connection=True;")
      .Options;
  }
}
