using System;
using System.Collections.Specialized;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Quartz;
using Xunit;

namespace AppAny.Quartz.EntityFrameworkCore.Migrations.Tests.SQLite;

public class SQLiteIntegrationDbContext_IntegrationTests : IDisposable
{
  private readonly SQLiteIntegrationDbContext _dbContext;
  private readonly string _connectionString;

  public SQLiteIntegrationDbContext_IntegrationTests()
  {
    // Database is created in [Root]/tests/AppAny.Quartz.EntityFrameworkCore.Migrations.Tests/bin/Debug/net6.0/
    _connectionString = new SqliteConnectionStringBuilder()
    {
      Mode = SqliteOpenMode.ReadWriteCreate,
      ForeignKeys = true,
      DataSource = "sqlite_test.db",
      Cache = SqliteCacheMode.Shared,
    }.ToString();

    var options = new DbContextOptionsBuilder<SQLiteIntegrationDbContext>().UseSqlite(_connectionString).Options;
    _dbContext = new SQLiteIntegrationDbContext(options);
  }

  [Fact]
  public void ShouldCompleteDatabaseMigrate_WhenDatabaseIsSQLite()
  {
    var exception = Record.Exception(() => _dbContext.Database.Migrate());

    Assert.Null(exception);
  }

  [Fact]
  public async Task ShouldBuildScheduler_WhenDataStoreIsSQLite()
  {
    // EnsureCreated is used here as Quartz does the SchemaValidation on scheduler creation
    var exception = Record.Exception(() => _dbContext.Database.EnsureCreated());
    Assert.Null(exception);

    // Create a Quartz scheduler with the above DBContext as the JobStore
    var scheduler = await SchedulerBuilder.Create()
      .UseDefaultThreadPool(x => x.MaxConcurrency = 5)
      .UsePersistentStore(x =>
      {
        x.PerformSchemaValidation = true;
        x.UseMicrosoftSQLite(_connectionString);
        x.UseJsonSerializer();
      })
      .BuildScheduler();

    // Start Scheduler, ensure no SQLite schema validation exceptions are thrown
    exception = await Record.ExceptionAsync(async () => await scheduler.Start());
    Assert.Null(exception);

    Assert.True(scheduler.IsStarted);

    await scheduler.Shutdown();
    Assert.True(scheduler.IsShutdown);
  }

  public void Dispose()
  {
    _dbContext.Database.EnsureDeleted();
    _dbContext.Dispose();
  }
}
