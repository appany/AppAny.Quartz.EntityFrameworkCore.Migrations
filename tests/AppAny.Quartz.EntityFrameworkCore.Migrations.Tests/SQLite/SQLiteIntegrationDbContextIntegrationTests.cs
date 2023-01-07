using System;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Quartz;
using Xunit;

namespace AppAny.Quartz.EntityFrameworkCore.Migrations.Tests.SQLite
{
  public class SQLiteIntegrationDbContextIntegrationTests : IDisposable
  {
    private readonly SQLiteIntegrationDbContext _dbContext;
    private readonly string _connectionString;

    public SQLiteIntegrationDbContextIntegrationTests()
    {
      // Database is created in [Root]/tests/AppAny.Quartz.EntityFrameworkCore.Migrations.Tests/bin/Debug/net6.0/
      _connectionString = new SqliteConnectionStringBuilder
      {
        Mode = SqliteOpenMode.ReadWriteCreate,
        ForeignKeys = true,
        DataSource = "sqlite_test.db",
        Cache = SqliteCacheMode.Shared
      }.ToString();

      var options = new DbContextOptionsBuilder<SQLiteIntegrationDbContext>().UseSqlite(_connectionString).Options;
      _dbContext = new SQLiteIntegrationDbContext(options);
    }

    [Fact]
    public async Task ShouldBuildScheduler()
    {
      // Arrange
      await _dbContext.Database.EnsureCreatedAsync();
      await _dbContext.Database.MigrateAsync();

      // Act
      var scheduler = await SchedulerBuilder.Create()
        .UseDefaultThreadPool(x => x.MaxConcurrency = 5)
        .UsePersistentStore(
          x =>
          {
            x.PerformSchemaValidation = true;
            x.UseMicrosoftSQLite(_connectionString);
            x.UseJsonSerializer();
          })
        .BuildScheduler();

      var exception = await Record.ExceptionAsync(async () => await scheduler.Start());

      // Assert
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
}
