namespace AppAny.Quartz.EntityFrameworkCore.Migrations.SQLite.Tests
{
  using System;
      using System.Collections.Generic;
  using System.Threading.Tasks;
      using System.Linq;
      using AppAny.Quartz.EntityFrameworkCore.Migrations.SQLite;
  using global::Quartz;
  using Microsoft.Data.Sqlite;
  using Microsoft.EntityFrameworkCore;
      using Microsoft.EntityFrameworkCore.Infrastructure;
      using Microsoft.EntityFrameworkCore.Metadata;
      using Microsoft.EntityFrameworkCore.Metadata.Conventions;
      using Microsoft.EntityFrameworkCore.Migrations;
      using Microsoft.EntityFrameworkCore.Migrations.Operations;
  using Xunit;

  public class SQLiteIntegrationDbContextIntegrationTests : IDisposable
  {
    private readonly SQLiteIntegrationDbContext _dbContext;
    private readonly string _connectionString;

    public SQLiteIntegrationDbContextIntegrationTests()
    {
      // Database is created in [Root]/tests/AppAny.Quartz.EntityFrameworkCore.Migrations.Tests/bin/Debug/net6.0/
      this._connectionString = new SqliteConnectionStringBuilder
      {
        Mode = SqliteOpenMode.ReadWriteCreate,
        ForeignKeys = true,
        DataSource = "sqlite_test.db",
        Cache = SqliteCacheMode.Shared
      }.ToString();

      var options = new DbContextOptionsBuilder<SQLiteIntegrationDbContext>().UseSqlite(this._connectionString).Options;
      this._dbContext = new SQLiteIntegrationDbContext(options);
    }

#if QUARTZ_4_2
      [Theory]
      [InlineData(false)]
      [InlineData(true)]
      public async Task ShouldBuildScheduler(bool useExecutionHistory)
#else
      [Fact]
      public async Task ShouldBuildScheduler()
#endif
    {
      // Arrange
      await this._dbContext.Database.EnsureCreatedAsync();
      await this._dbContext.Database.MigrateAsync();

      // Act
#if QUARTZ_4
      var scheduler = await QuartzSchedulerBuilder.Create(q => q
#else
      var scheduler = await SchedulerBuilder.Create()
#endif
        .UseDefaultThreadPool(x => x.MaxConcurrency = 5)
        .UsePersistentStore(
          x =>
          {
#if QUARTZ_4
            x.ConfigureStore(options => options.SchemaProvisioning = SchemaProvisioning.Validate);
#else
            x.PerformSchemaValidation = true;
#endif
#if QUARTZ_4_2
                                    if (useExecutionHistory)
                                    {
                                          x.UseExecutionHistory();
                                    }
#endif
#if QUARTZ_4
            x.UseSqlite(this._connectionString);
#else
            x.UseMicrosoftSQLite(this._connectionString);
#endif
            x.UseNewtonsoftJsonSerializer();
          })
#if QUARTZ_4
        )
#endif
        .BuildScheduler();

      var exception = await Record.ExceptionAsync(async () => await scheduler.Start());

      // Assert
      Assert.Null(exception);
#if QUARTZ_4
      Assert.Equal(SchedulerStatus.Running, scheduler.Status);
#else
      Assert.True(scheduler.IsStarted);
#endif

      await scheduler.Shutdown();
#if QUARTZ_4
      Assert.Equal(SchedulerStatus.Shutdown, scheduler.Status);
#else
      Assert.True(scheduler.IsShutdown);
#endif
    }

            [Fact]
            public async Task ShouldUpgradeQuartz40SchemaWithoutChangingExistingRows()
            {
                  var oldModelBuilder = new ModelBuilder(ConventionSet.CreateConventionSet(this._dbContext));
                  oldModelBuilder.AddQuartz(quartz => quartz.UseSqlite());
                  oldModelBuilder.Ignore<QuartzExecutionHistory>();
                  oldModelBuilder.Ignore<QuartzMisfireHistory>();
                  oldModelBuilder.Entity<QuartzTrigger>()
                        .Ignore(trigger => trigger.ContinuesTriggerName)
                        .Ignore(trigger => trigger.ContinuesTriggerGroup)
                        .Ignore(trigger => trigger.ContinuationCondition)
                        .Ignore(trigger => trigger.OverlapPolicy)
                        .Ignore(trigger => trigger.PauseReason)
                        .Ignore(trigger => trigger.PausedBy)
                        .Ignore(trigger => trigger.PausedAt);
                  oldModelBuilder.Entity<QuartzFiredTrigger>()
                        .Ignore(trigger => trigger.Progress)
                        .Ignore(trigger => trigger.ProgressMessage);
                  oldModelBuilder.Entity<QuartzPausedTriggerGroup>()
                        .Ignore(group => group.PauseReason)
                        .Ignore(group => group.PausedBy)
                        .Ignore(group => group.PausedAt);
                  oldModelBuilder.Entity<QuartzPausedJobGroup>()
                        .Ignore(group => group.PauseReason)
                        .Ignore(group => group.PausedBy)
                        .Ignore(group => group.PausedAt);

                  var oldModel = this._dbContext.GetService<IModelRuntimeInitializer>()
                        .Initialize(oldModelBuilder.FinalizeModel(), designTime: true);
                  var newModel = this._dbContext.GetService<IDesignTimeModel>().Model;
                  var modelDiffer = this._dbContext.GetService<IMigrationsModelDiffer>();

                  await ExecuteMigrationOperations(
                        modelDiffer.GetDifferences(null, oldModel.GetRelationalModel()), oldModel);

                  this._dbContext.Set<QuartzJobDetail>().Add(new QuartzJobDetail
                  {
                        SchedulerName = "existing_scheduler",
                        JobName = "existing_job",
                        JobGroup = "existing_group",
                        JobClassName = "ExistingJob",
                        IsDurable = true
                  });
                  await this._dbContext.SaveChangesAsync();

                  var seedMigration = new MigrationBuilder(this._dbContext.Database.ProviderName!);
                  seedMigration.InsertData("QRTZ_TRIGGERS",
                        new[] { "SCHED_NAME", "TRIGGER_NAME", "TRIGGER_GROUP", "JOB_NAME", "JOB_GROUP", "TRIGGER_STATE", "TRIGGER_TYPE", "START_TIME" },
                        new object[] { "existing_scheduler", "existing_trigger", "existing_group", "existing_job", "existing_group", "WAITING", "SIMPLE", 123L });
                  await ExecuteMigrationOperations(seedMigration.Operations, oldModel);

                  var upgradeOperations = modelDiffer.GetDifferences(oldModel.GetRelationalModel(), newModel.GetRelationalModel());
                  Assert.Equal(2, upgradeOperations.OfType<CreateTableOperation>().Count());
                  Assert.Equal(15, upgradeOperations.OfType<AddColumnOperation>().Count());
                  Assert.Equal(4, upgradeOperations.OfType<CreateIndexOperation>().Count());
                  Assert.All(upgradeOperations, operation =>
                        Assert.True(operation is CreateTableOperation or AddColumnOperation or CreateIndexOperation));
                  Assert.All(upgradeOperations.OfType<AddColumnOperation>(), column => Assert.True(column.IsNullable));

                  await ExecuteMigrationOperations(upgradeOperations, newModel);
                  this._dbContext.ChangeTracker.Clear();

                  var job = await this._dbContext.Set<QuartzJobDetail>().SingleAsync();
                  Assert.Equal("existing_job", job.JobName);
                  Assert.True(job.IsDurable);

                  var trigger = await this._dbContext.Set<QuartzTrigger>().SingleAsync();
                  Assert.Equal("existing_trigger", trigger.TriggerName);
                  Assert.Equal(123L, trigger.StartTime);
                  Assert.Equal("WAITING", trigger.TriggerState);
                  Assert.Null(trigger.ContinuesTriggerName);
                  Assert.Null(trigger.ContinuesTriggerGroup);
                  Assert.Null(trigger.ContinuationCondition);
                  Assert.Null(trigger.OverlapPolicy);
                  Assert.Null(trigger.PauseReason);
                  Assert.Null(trigger.PausedBy);
                  Assert.Null(trigger.PausedAt);
                  Assert.Empty(await this._dbContext.Set<QuartzExecutionHistory>().ToListAsync());
                  Assert.Empty(await this._dbContext.Set<QuartzMisfireHistory>().ToListAsync());
            }

            private async Task ExecuteMigrationOperations(IReadOnlyList<MigrationOperation> operations, IModel model)
            {
                  var sqlGenerator = this._dbContext.GetService<IMigrationsSqlGenerator>();
                  foreach (var command in sqlGenerator.Generate(operations, model))
                  {
                        await this._dbContext.Database.ExecuteSqlRawAsync(command.CommandText);
                  }
            }

    public void Dispose()
    {
      this._dbContext.Database.EnsureDeleted();
      this._dbContext.Dispose();
    }
  }
}
