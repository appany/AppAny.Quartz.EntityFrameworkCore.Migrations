namespace AppAny.Quartz.EntityFrameworkCore.Migrations.PostgreSQL.Tests;

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

  // Index set of the Quartz.NET 4.0.1 table scripts (database/tables/tables_postgres.sql)
  [Fact]
  public void ShouldMapQuartzIndexes()
  {
    using var dbContext = new PostgreSqlIntegrationDbContext(CreateOptions());

    Assert.Equal(
      new[] { "idx_qrtz_j_g_n (sched_name, job_group, job_name)" },
      GetIndexes<QuartzJobDetail>(dbContext));

    Assert.Equal(
      new[]
      {
        "idx_qrtz_t_c (sched_name, calendar_name)",
        "idx_qrtz_t_g_n (sched_name, trigger_group, trigger_name)",
        "idx_qrtz_t_j (sched_name, job_name, job_group)",
        "idx_qrtz_t_nft_st (sched_name, trigger_state, next_fire_time, priority DESC, misfire_instr)"
      },
      GetIndexes<QuartzTrigger>(dbContext));

    Assert.Equal(
      new[]
      {
        "idx_qrtz_ft_inst_job_req_rcvry (sched_name, instance_name, requests_recovery)",
        "idx_qrtz_ft_j_g (sched_name, job_name, job_group)",
        "idx_qrtz_ft_t_g (sched_name, trigger_name, trigger_group)"
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

  private static DbContextOptions<PostgreSqlIntegrationDbContext> CreateOptions()
  {
    return new DbContextOptionsBuilder<PostgreSqlIntegrationDbContext>()
      .UseNpgsql("Host=localhost;Port=5432;Database=quartz_mapping_tests;Username=postgres;Password=postgres")
      .Options;
  }
}
