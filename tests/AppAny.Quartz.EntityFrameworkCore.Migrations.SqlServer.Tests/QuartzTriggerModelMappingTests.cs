namespace AppAny.Quartz.EntityFrameworkCore.Migrations.SqlServer.Tests;

using System;
using System.Linq;
using AppAny.Quartz.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore;
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

  [Theory]
  [InlineData(nameof(QuartzTrigger.ContinuesTriggerName), "CONTINUES_TRIGGER_NAME", "nvarchar(150)")]
  [InlineData(nameof(QuartzTrigger.ContinuesTriggerGroup), "CONTINUES_TRIGGER_GROUP", "nvarchar(150)")]
  [InlineData(nameof(QuartzTrigger.ContinuationCondition), "CONTINUATION_CONDITION", "int")]
  [InlineData(nameof(QuartzTrigger.OverlapPolicy), "OVERLAP_POLICY", "int")]
  [InlineData(nameof(QuartzTrigger.PauseReason), "PAUSE_REASON", "nvarchar(250)")]
  [InlineData(nameof(QuartzTrigger.PausedBy), "PAUSED_BY", "nvarchar(200)")]
  [InlineData(nameof(QuartzTrigger.PausedAt), "PAUSED_AT", "bigint")]
  public void ShouldMapQuartz42And43TriggerColumns(string propertyName, string columnName, string columnType)
  {
    using var dbContext = new SqlServerIntegrationDbContext(CreateOptions());
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
  [InlineData(typeof(QuartzFiredTrigger), nameof(QuartzFiredTrigger.Progress), "PROGRESS", "int")]
  [InlineData(typeof(QuartzFiredTrigger), nameof(QuartzFiredTrigger.ProgressMessage), "PROGRESS_MESSAGE", "nvarchar(250)")]
  [InlineData(typeof(QuartzPausedTriggerGroup), nameof(QuartzPausedTriggerGroup.PauseReason), "PAUSE_REASON", "nvarchar(250)")]
  [InlineData(typeof(QuartzPausedTriggerGroup), nameof(QuartzPausedTriggerGroup.PausedBy), "PAUSED_BY", "nvarchar(200)")]
  [InlineData(typeof(QuartzPausedTriggerGroup), nameof(QuartzPausedTriggerGroup.PausedAt), "PAUSED_AT", "bigint")]
  [InlineData(typeof(QuartzPausedJobGroup), nameof(QuartzPausedJobGroup.PauseReason), "PAUSE_REASON", "nvarchar(250)")]
  [InlineData(typeof(QuartzPausedJobGroup), nameof(QuartzPausedJobGroup.PausedBy), "PAUSED_BY", "nvarchar(200)")]
  [InlineData(typeof(QuartzPausedJobGroup), nameof(QuartzPausedJobGroup.PausedAt), "PAUSED_AT", "bigint")]
  public void ShouldMapQuartz43ProgressAndPauseColumns(Type entityClrType, string propertyName,
    string columnName, string columnType)
  {
    using var dbContext = new SqlServerIntegrationDbContext(CreateOptions());
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
    using var dbContext = new SqlServerIntegrationDbContext(CreateOptions());
    var entityType = dbContext.Model.FindEntityType(entityClrType);

    Assert.NotNull(entityType);
    Assert.Equal(tableName, entityType!.GetTableName());
    Assert.Equal(new[] { "SchedulerName", "EntryId" },
      entityType.FindPrimaryKey()!.Properties.Select(property => property.Name));
    Assert.Equal(ValueGenerated.Never, entityType.FindProperty("EntryId")!.ValueGenerated);
    Assert.Empty(entityType.GetForeignKeys());
  }

  [Theory]
  [InlineData(typeof(QuartzExecutionHistory), nameof(QuartzExecutionHistory.SchedulerName), "SCHED_NAME", "nvarchar(120)", false)]
  [InlineData(typeof(QuartzExecutionHistory), nameof(QuartzExecutionHistory.EntryId), "ENTRY_ID", "nvarchar(140)", false)]
  [InlineData(typeof(QuartzExecutionHistory), nameof(QuartzExecutionHistory.InstanceName), "INSTANCE_NAME", "nvarchar(200)", false)]
  [InlineData(typeof(QuartzExecutionHistory), nameof(QuartzExecutionHistory.JobName), "JOB_NAME", "nvarchar(150)", false)]
  [InlineData(typeof(QuartzExecutionHistory), nameof(QuartzExecutionHistory.JobGroup), "JOB_GROUP", "nvarchar(150)", false)]
  [InlineData(typeof(QuartzExecutionHistory), nameof(QuartzExecutionHistory.TriggerName), "TRIGGER_NAME", "nvarchar(150)", false)]
  [InlineData(typeof(QuartzExecutionHistory), nameof(QuartzExecutionHistory.TriggerGroup), "TRIGGER_GROUP", "nvarchar(150)", false)]
  [InlineData(typeof(QuartzExecutionHistory), nameof(QuartzExecutionHistory.FiredTime), "FIRED_TIME", "bigint", false)]
  [InlineData(typeof(QuartzExecutionHistory), nameof(QuartzExecutionHistory.RunTime), "RUN_TIME", "bigint", false)]
  [InlineData(typeof(QuartzExecutionHistory), nameof(QuartzExecutionHistory.Succeeded), "SUCCEEDED", "bit", false)]
  [InlineData(typeof(QuartzExecutionHistory), nameof(QuartzExecutionHistory.ErrorMessage), "ERROR_MESSAGE", "nvarchar(1000)", true)]
  [InlineData(typeof(QuartzExecutionHistory), nameof(QuartzExecutionHistory.RetryAttempt), "RETRY_ATTEMPT", "int", false)]
  [InlineData(typeof(QuartzExecutionHistory), nameof(QuartzExecutionHistory.RetryScheduled), "RETRY_SCHEDULED", "bit", false)]
  [InlineData(typeof(QuartzExecutionHistory), nameof(QuartzExecutionHistory.ExecutionLog), "EXECUTION_LOG", "nvarchar(max)", true)]
  [InlineData(typeof(QuartzMisfireHistory), nameof(QuartzMisfireHistory.SchedulerName), "SCHED_NAME", "nvarchar(120)", false)]
  [InlineData(typeof(QuartzMisfireHistory), nameof(QuartzMisfireHistory.EntryId), "ENTRY_ID", "nvarchar(140)", false)]
  [InlineData(typeof(QuartzMisfireHistory), nameof(QuartzMisfireHistory.InstanceName), "INSTANCE_NAME", "nvarchar(200)", false)]
  [InlineData(typeof(QuartzMisfireHistory), nameof(QuartzMisfireHistory.TriggerName), "TRIGGER_NAME", "nvarchar(150)", false)]
  [InlineData(typeof(QuartzMisfireHistory), nameof(QuartzMisfireHistory.TriggerGroup), "TRIGGER_GROUP", "nvarchar(150)", false)]
  [InlineData(typeof(QuartzMisfireHistory), nameof(QuartzMisfireHistory.JobName), "JOB_NAME", "nvarchar(150)", true)]
  [InlineData(typeof(QuartzMisfireHistory), nameof(QuartzMisfireHistory.JobGroup), "JOB_GROUP", "nvarchar(150)", true)]
  [InlineData(typeof(QuartzMisfireHistory), nameof(QuartzMisfireHistory.MisfireTime), "MISFIRE_TIME", "bigint", false)]
  [InlineData(typeof(QuartzMisfireHistory), nameof(QuartzMisfireHistory.ScheduledTime), "SCHED_TIME", "bigint", true)]
  [InlineData(typeof(QuartzMisfireHistory), nameof(QuartzMisfireHistory.Reason), "REASON", "int", true)]
  public void ShouldMapHistoryColumns(Type entityClrType, string propertyName, string columnName,
    string columnType, bool isNullable)
  {
    using var dbContext = new SqlServerIntegrationDbContext(CreateOptions());
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
    using var dbContext = new SqlServerIntegrationDbContext(CreateOptions());
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
    using var dbContext = new SqlServerIntegrationDbContext(CreateOptions());
    var entityType = dbContext.Model.FindEntityType(entityClrType);

    Assert.NotNull(entityType);
    Assert.Contains(entityType!.GetIndexes(), index => index.GetDatabaseName() == indexName
      && index.Properties.Select(property => property.Name).SequenceEqual(new[] { "SchedulerName", propertyName }));
  }

  private static DbContextOptions<SqlServerIntegrationDbContext> CreateOptions()
  {
    return new DbContextOptionsBuilder<SqlServerIntegrationDbContext>()
      .UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=QuartzMappingTests;Trusted_Connection=True;")
      .Options;
  }
}
