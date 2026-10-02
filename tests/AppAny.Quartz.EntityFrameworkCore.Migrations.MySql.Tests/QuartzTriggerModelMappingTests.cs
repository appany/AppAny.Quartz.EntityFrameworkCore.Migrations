namespace AppAny.Quartz.EntityFrameworkCore.Migrations.MySql.Tests;

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
    using var dbContext = new MySqlIntegrationDbContext(CreateOptions());
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

  // Index set of the Quartz.NET 4.0.1 table scripts (database/tables/tables_mysql_innodb.sql)
  [Fact]
  public void ShouldMapQuartzIndexes()
  {
    using var dbContext = new MySqlIntegrationDbContext(CreateOptions());

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

  [Theory]
  [InlineData(nameof(QuartzTrigger.ContinuesTriggerName), "CONTINUES_TRIGGER_NAME", "varchar(200)")]
  [InlineData(nameof(QuartzTrigger.ContinuesTriggerGroup), "CONTINUES_TRIGGER_GROUP", "varchar(200)")]
  [InlineData(nameof(QuartzTrigger.ContinuationCondition), "CONTINUATION_CONDITION", "integer")]
  [InlineData(nameof(QuartzTrigger.OverlapPolicy), "OVERLAP_POLICY", "integer")]
  [InlineData(nameof(QuartzTrigger.PauseReason), "PAUSE_REASON", "varchar(250)")]
  [InlineData(nameof(QuartzTrigger.PausedBy), "PAUSED_BY", "varchar(200)")]
  [InlineData(nameof(QuartzTrigger.PausedAt), "PAUSED_AT", "bigint(19)")]
  public void ShouldMapQuartz42And43TriggerColumns(string propertyName, string columnName, string columnType)
  {
    using var dbContext = new MySqlIntegrationDbContext(CreateOptions());
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
  [InlineData(typeof(QuartzFiredTrigger), nameof(QuartzFiredTrigger.Progress), "PROGRESS", "integer")]
  [InlineData(typeof(QuartzFiredTrigger), nameof(QuartzFiredTrigger.ProgressMessage), "PROGRESS_MESSAGE", "varchar(250)")]
  [InlineData(typeof(QuartzPausedTriggerGroup), nameof(QuartzPausedTriggerGroup.PauseReason), "PAUSE_REASON", "varchar(250)")]
  [InlineData(typeof(QuartzPausedTriggerGroup), nameof(QuartzPausedTriggerGroup.PausedBy), "PAUSED_BY", "varchar(200)")]
  [InlineData(typeof(QuartzPausedTriggerGroup), nameof(QuartzPausedTriggerGroup.PausedAt), "PAUSED_AT", "bigint(19)")]
  [InlineData(typeof(QuartzPausedJobGroup), nameof(QuartzPausedJobGroup.PauseReason), "PAUSE_REASON", "varchar(250)")]
  [InlineData(typeof(QuartzPausedJobGroup), nameof(QuartzPausedJobGroup.PausedBy), "PAUSED_BY", "varchar(200)")]
  [InlineData(typeof(QuartzPausedJobGroup), nameof(QuartzPausedJobGroup.PausedAt), "PAUSED_AT", "bigint(19)")]
  public void ShouldMapQuartz43ProgressAndPauseColumns(Type entityClrType, string propertyName,
    string columnName, string columnType)
  {
    using var dbContext = new MySqlIntegrationDbContext(CreateOptions());
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
  [InlineData(typeof(QuartzExecutionHistory), "QRTZ_EXECUTION_HISTORY")]
  [InlineData(typeof(QuartzMisfireHistory), "QRTZ_MISFIRE_HISTORY")]
  public void ShouldMapHistoryTablesWithoutForeignKeys(Type entityClrType, string tableName)
  {
    using var dbContext = new MySqlIntegrationDbContext(CreateOptions());
    var entityType = dbContext.Model.FindEntityType(entityClrType);

    Assert.NotNull(entityType);
    Assert.Equal(tableName, entityType!.GetTableName());
    Assert.Equal(new[] { "SchedulerName", "EntryId" },
      entityType.FindPrimaryKey()!.Properties.Select(property => property.Name));
    Assert.Equal(ValueGenerated.Never, entityType.FindProperty("EntryId")!.ValueGenerated);
    Assert.Empty(entityType.GetForeignKeys());
  }

  [Theory]
  [InlineData(typeof(QuartzExecutionHistory), nameof(QuartzExecutionHistory.SchedulerName), "SCHED_NAME", "varchar(120)", false)]
  [InlineData(typeof(QuartzExecutionHistory), nameof(QuartzExecutionHistory.EntryId), "ENTRY_ID", "varchar(140)", false)]
  [InlineData(typeof(QuartzExecutionHistory), nameof(QuartzExecutionHistory.InstanceName), "INSTANCE_NAME", "varchar(200)", false)]
  [InlineData(typeof(QuartzExecutionHistory), nameof(QuartzExecutionHistory.JobName), "JOB_NAME", "varchar(200)", false)]
  [InlineData(typeof(QuartzExecutionHistory), nameof(QuartzExecutionHistory.JobGroup), "JOB_GROUP", "varchar(200)", false)]
  [InlineData(typeof(QuartzExecutionHistory), nameof(QuartzExecutionHistory.TriggerName), "TRIGGER_NAME", "varchar(200)", false)]
  [InlineData(typeof(QuartzExecutionHistory), nameof(QuartzExecutionHistory.TriggerGroup), "TRIGGER_GROUP", "varchar(200)", false)]
  [InlineData(typeof(QuartzExecutionHistory), nameof(QuartzExecutionHistory.FiredTime), "FIRED_TIME", "bigint(19)", false)]
  [InlineData(typeof(QuartzExecutionHistory), nameof(QuartzExecutionHistory.RunTime), "RUN_TIME", "bigint(19)", false)]
  [InlineData(typeof(QuartzExecutionHistory), nameof(QuartzExecutionHistory.Succeeded), "SUCCEEDED", "tinyint(1)", false)]
  [InlineData(typeof(QuartzExecutionHistory), nameof(QuartzExecutionHistory.ErrorMessage), "ERROR_MESSAGE", "varchar(1000)", true)]
  [InlineData(typeof(QuartzExecutionHistory), nameof(QuartzExecutionHistory.RetryAttempt), "RETRY_ATTEMPT", "integer", false)]
  [InlineData(typeof(QuartzExecutionHistory), nameof(QuartzExecutionHistory.RetryScheduled), "RETRY_SCHEDULED", "tinyint(1)", false)]
  [InlineData(typeof(QuartzExecutionHistory), nameof(QuartzExecutionHistory.ExecutionLog), "EXECUTION_LOG", "longtext", true)]
  [InlineData(typeof(QuartzMisfireHistory), nameof(QuartzMisfireHistory.SchedulerName), "SCHED_NAME", "varchar(120)", false)]
  [InlineData(typeof(QuartzMisfireHistory), nameof(QuartzMisfireHistory.EntryId), "ENTRY_ID", "varchar(140)", false)]
  [InlineData(typeof(QuartzMisfireHistory), nameof(QuartzMisfireHistory.InstanceName), "INSTANCE_NAME", "varchar(200)", false)]
  [InlineData(typeof(QuartzMisfireHistory), nameof(QuartzMisfireHistory.TriggerName), "TRIGGER_NAME", "varchar(200)", false)]
  [InlineData(typeof(QuartzMisfireHistory), nameof(QuartzMisfireHistory.TriggerGroup), "TRIGGER_GROUP", "varchar(200)", false)]
  [InlineData(typeof(QuartzMisfireHistory), nameof(QuartzMisfireHistory.JobName), "JOB_NAME", "varchar(200)", true)]
  [InlineData(typeof(QuartzMisfireHistory), nameof(QuartzMisfireHistory.JobGroup), "JOB_GROUP", "varchar(200)", true)]
  [InlineData(typeof(QuartzMisfireHistory), nameof(QuartzMisfireHistory.MisfireTime), "MISFIRE_TIME", "bigint(19)", false)]
  [InlineData(typeof(QuartzMisfireHistory), nameof(QuartzMisfireHistory.ScheduledTime), "SCHED_TIME", "bigint(19)", true)]
  [InlineData(typeof(QuartzMisfireHistory), nameof(QuartzMisfireHistory.Reason), "REASON", "integer", true)]
  public void ShouldMapHistoryColumns(Type entityClrType, string propertyName, string columnName,
    string columnType, bool isNullable)
  {
    using var dbContext = new MySqlIntegrationDbContext(CreateOptions());
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
    using var dbContext = new MySqlIntegrationDbContext(CreateOptions());
    var entityType = dbContext.Model.FindEntityType(typeof(QuartzExecutionHistory));

    Assert.NotNull(entityType);
    Assert.Equal(0, entityType!.FindProperty(nameof(QuartzExecutionHistory.RetryAttempt))!.GetDefaultValue());
    Assert.Equal(false, entityType.FindProperty(nameof(QuartzExecutionHistory.RetryScheduled))!.GetDefaultValue());
  }

  [Theory]
  [InlineData(typeof(QuartzExecutionHistory), "IDX_QRTZ_EH_FIRED_TIME", nameof(QuartzExecutionHistory.FiredTime))]
  [InlineData(typeof(QuartzExecutionHistory), "IDX_QRTZ_EH_INST", nameof(QuartzExecutionHistory.InstanceName))]
  [InlineData(typeof(QuartzMisfireHistory), "IDX_QRTZ_MH_MISFIRE_TIME", nameof(QuartzMisfireHistory.MisfireTime))]
  [InlineData(typeof(QuartzMisfireHistory), "IDX_QRTZ_MH_INST", nameof(QuartzMisfireHistory.InstanceName))]
  public void ShouldMapHistoryIndexes(Type entityClrType, string indexName, string propertyName)
  {
    using var dbContext = new MySqlIntegrationDbContext(CreateOptions());
    var entityType = dbContext.Model.FindEntityType(entityClrType);

    Assert.NotNull(entityType);
    Assert.Contains(entityType!.GetIndexes(), index => index.GetDatabaseName() == indexName
      && index.Properties.Select(property => property.Name).SequenceEqual(new[] { "SchedulerName", propertyName }));
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
