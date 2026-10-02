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

  [Theory]
  [InlineData(nameof(QuartzTrigger.ContinuesTriggerName), "continues_trigger_name", "text")]
  [InlineData(nameof(QuartzTrigger.ContinuesTriggerGroup), "continues_trigger_group", "text")]
  [InlineData(nameof(QuartzTrigger.ContinuationCondition), "continuation_condition", "integer")]
  [InlineData(nameof(QuartzTrigger.OverlapPolicy), "overlap_policy", "integer")]
  [InlineData(nameof(QuartzTrigger.PauseReason), "pause_reason", "varchar(250)")]
  [InlineData(nameof(QuartzTrigger.PausedBy), "paused_by", "varchar(200)")]
  [InlineData(nameof(QuartzTrigger.PausedAt), "paused_at", "bigint")]
  public void ShouldMapQuartz42And43TriggerColumns(string propertyName, string columnName, string columnType)
  {
    using var dbContext = new PostgreSqlIntegrationDbContext(CreateOptions());
    var entityType = dbContext.Model.FindEntityType(typeof(QuartzTrigger));

    Assert.NotNull(entityType);

    var property = entityType!.FindProperty(propertyName);
    Assert.NotNull(property);

    var table = StoreObjectIdentifier.Table(entityType.GetTableName()!, entityType.GetSchema());
    Assert.Equal(columnName, property!.GetColumnName(table));
    Assert.Equal(columnType, property.GetColumnType());
    Assert.True(property.IsNullable);
    Assert.Null(property.GetDefaultValue());
  }

  [Theory]
  [InlineData(typeof(QuartzFiredTrigger), nameof(QuartzFiredTrigger.Progress), "progress", "integer")]
  [InlineData(typeof(QuartzFiredTrigger), nameof(QuartzFiredTrigger.ProgressMessage), "progress_message", "varchar(250)")]
  [InlineData(typeof(QuartzPausedTriggerGroup), nameof(QuartzPausedTriggerGroup.PauseReason), "pause_reason", "varchar(250)")]
  [InlineData(typeof(QuartzPausedTriggerGroup), nameof(QuartzPausedTriggerGroup.PausedBy), "paused_by", "varchar(200)")]
  [InlineData(typeof(QuartzPausedTriggerGroup), nameof(QuartzPausedTriggerGroup.PausedAt), "paused_at", "bigint")]
  [InlineData(typeof(QuartzPausedJobGroup), nameof(QuartzPausedJobGroup.PauseReason), "pause_reason", "varchar(250)")]
  [InlineData(typeof(QuartzPausedJobGroup), nameof(QuartzPausedJobGroup.PausedBy), "paused_by", "varchar(200)")]
  [InlineData(typeof(QuartzPausedJobGroup), nameof(QuartzPausedJobGroup.PausedAt), "paused_at", "bigint")]
  public void ShouldMapQuartz43ProgressAndPauseColumns(Type entityClrType, string propertyName,
    string columnName, string columnType)
  {
    using var dbContext = new PostgreSqlIntegrationDbContext(CreateOptions());
    var entityType = dbContext.Model.FindEntityType(entityClrType);

    Assert.NotNull(entityType);

    var property = entityType!.FindProperty(propertyName);
    Assert.NotNull(property);

    var table = StoreObjectIdentifier.Table(entityType.GetTableName()!, entityType.GetSchema());
    Assert.Equal(columnName, property!.GetColumnName(table));
    Assert.Equal(columnType, property.GetColumnType());
    Assert.True(property.IsNullable);
    Assert.Null(property.GetDefaultValue());
  }

  [Theory]
  [InlineData(typeof(QuartzExecutionHistory), "qrtz_execution_history")]
  [InlineData(typeof(QuartzMisfireHistory), "qrtz_misfire_history")]
  public void ShouldMapHistoryTablesWithoutForeignKeys(Type entityClrType, string tableName)
  {
    using var dbContext = new PostgreSqlIntegrationDbContext(CreateOptions());
    var entityType = dbContext.Model.FindEntityType(entityClrType);

    Assert.NotNull(entityType);
    Assert.Equal(tableName, entityType!.GetTableName());
    Assert.Equal(new[] { "SchedulerName", "EntryId" },
      entityType.FindPrimaryKey()!.Properties.Select(property => property.Name));
    Assert.Equal(ValueGenerated.Never, entityType.FindProperty("EntryId")!.ValueGenerated);
    Assert.Empty(entityType.GetForeignKeys());
  }

  [Theory]
  [InlineData(typeof(QuartzExecutionHistory), nameof(QuartzExecutionHistory.SchedulerName), "sched_name", "text", false)]
  [InlineData(typeof(QuartzExecutionHistory), nameof(QuartzExecutionHistory.EntryId), "entry_id", "text", false)]
  [InlineData(typeof(QuartzExecutionHistory), nameof(QuartzExecutionHistory.InstanceName), "instance_name", "text", false)]
  [InlineData(typeof(QuartzExecutionHistory), nameof(QuartzExecutionHistory.JobName), "job_name", "text", false)]
  [InlineData(typeof(QuartzExecutionHistory), nameof(QuartzExecutionHistory.JobGroup), "job_group", "text", false)]
  [InlineData(typeof(QuartzExecutionHistory), nameof(QuartzExecutionHistory.TriggerName), "trigger_name", "text", false)]
  [InlineData(typeof(QuartzExecutionHistory), nameof(QuartzExecutionHistory.TriggerGroup), "trigger_group", "text", false)]
  [InlineData(typeof(QuartzExecutionHistory), nameof(QuartzExecutionHistory.FiredTime), "fired_time", "bigint", false)]
  [InlineData(typeof(QuartzExecutionHistory), nameof(QuartzExecutionHistory.RunTime), "run_time", "bigint", false)]
  [InlineData(typeof(QuartzExecutionHistory), nameof(QuartzExecutionHistory.Succeeded), "succeeded", "bool", false)]
  [InlineData(typeof(QuartzExecutionHistory), nameof(QuartzExecutionHistory.ErrorMessage), "error_message", "text", true)]
  [InlineData(typeof(QuartzExecutionHistory), nameof(QuartzExecutionHistory.RetryAttempt), "retry_attempt", "integer", false)]
  [InlineData(typeof(QuartzExecutionHistory), nameof(QuartzExecutionHistory.RetryScheduled), "retry_scheduled", "bool", false)]
  [InlineData(typeof(QuartzExecutionHistory), nameof(QuartzExecutionHistory.ExecutionLog), "execution_log", "text", true)]
  [InlineData(typeof(QuartzMisfireHistory), nameof(QuartzMisfireHistory.SchedulerName), "sched_name", "text", false)]
  [InlineData(typeof(QuartzMisfireHistory), nameof(QuartzMisfireHistory.EntryId), "entry_id", "text", false)]
  [InlineData(typeof(QuartzMisfireHistory), nameof(QuartzMisfireHistory.InstanceName), "instance_name", "text", false)]
  [InlineData(typeof(QuartzMisfireHistory), nameof(QuartzMisfireHistory.TriggerName), "trigger_name", "text", false)]
  [InlineData(typeof(QuartzMisfireHistory), nameof(QuartzMisfireHistory.TriggerGroup), "trigger_group", "text", false)]
  [InlineData(typeof(QuartzMisfireHistory), nameof(QuartzMisfireHistory.JobName), "job_name", "text", true)]
  [InlineData(typeof(QuartzMisfireHistory), nameof(QuartzMisfireHistory.JobGroup), "job_group", "text", true)]
  [InlineData(typeof(QuartzMisfireHistory), nameof(QuartzMisfireHistory.MisfireTime), "misfire_time", "bigint", false)]
  [InlineData(typeof(QuartzMisfireHistory), nameof(QuartzMisfireHistory.ScheduledTime), "sched_time", "bigint", true)]
  [InlineData(typeof(QuartzMisfireHistory), nameof(QuartzMisfireHistory.Reason), "reason", "integer", true)]
  public void ShouldMapHistoryColumns(Type entityClrType, string propertyName, string columnName,
    string columnType, bool isNullable)
  {
    using var dbContext = new PostgreSqlIntegrationDbContext(CreateOptions());
    var entityType = dbContext.Model.FindEntityType(entityClrType);

    Assert.NotNull(entityType);

    var property = entityType!.FindProperty(propertyName);
    Assert.NotNull(property);

    var table = StoreObjectIdentifier.Table(entityType.GetTableName()!, entityType.GetSchema());
    Assert.Equal(columnName, property!.GetColumnName(table));
    Assert.Equal(columnType, property.GetColumnType());
    Assert.Equal(isNullable, property.IsNullable);
  }

  [Fact]
  public void ShouldMapHistoryRetryDefaults()
  {
    using var dbContext = new PostgreSqlIntegrationDbContext(CreateOptions());
    var entityType = dbContext.Model.FindEntityType(typeof(QuartzExecutionHistory));

    Assert.NotNull(entityType);
    Assert.Equal(0, entityType!.FindProperty(nameof(QuartzExecutionHistory.RetryAttempt))!.GetDefaultValue());
    Assert.Equal(false, entityType.FindProperty(nameof(QuartzExecutionHistory.RetryScheduled))!.GetDefaultValue());
  }

  [Theory]
  [InlineData(typeof(QuartzExecutionHistory), "idx_qrtz_eh_fired_time", nameof(QuartzExecutionHistory.FiredTime))]
  [InlineData(typeof(QuartzExecutionHistory), "idx_qrtz_eh_inst", nameof(QuartzExecutionHistory.InstanceName))]
  [InlineData(typeof(QuartzMisfireHistory), "idx_qrtz_mh_misfire_time", nameof(QuartzMisfireHistory.MisfireTime))]
  [InlineData(typeof(QuartzMisfireHistory), "idx_qrtz_mh_inst", nameof(QuartzMisfireHistory.InstanceName))]
  public void ShouldMapHistoryIndexes(Type entityClrType, string indexName, string propertyName)
  {
    using var dbContext = new PostgreSqlIntegrationDbContext(CreateOptions());
    var entityType = dbContext.Model.FindEntityType(entityClrType);

    Assert.NotNull(entityType);
    Assert.Contains(entityType!.GetIndexes(), index => index.GetDatabaseName() == indexName
      && index.Properties.Select(property => property.Name).SequenceEqual(new[] { "SchedulerName", propertyName }));
  }

  private static DbContextOptions<PostgreSqlIntegrationDbContext> CreateOptions()
  {
    return new DbContextOptionsBuilder<PostgreSqlIntegrationDbContext>()
      .UseNpgsql("Host=localhost;Port=5432;Database=quartz_mapping_tests;Username=postgres;Password=postgres")
      .Options;
  }
}
