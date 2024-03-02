namespace AppAny.Quartz.EntityFrameworkCore.Migrations.SQLite.Tests
{
  using System;
  using System.Threading.Tasks;
  using global::Quartz;
  using Microsoft.Data.Sqlite;
  using Microsoft.EntityFrameworkCore;
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

    [Fact]
    public async Task ShouldBuildScheduler()
    {
      // Arrange
      await this._dbContext.Database.EnsureCreatedAsync();
      await this._dbContext.Database.MigrateAsync();

      // Act
      var scheduler = await SchedulerBuilder.Create()
        .UseDefaultThreadPool(x => x.MaxConcurrency = 5)
        .UsePersistentStore(
          x =>
          {
            x.PerformSchemaValidation = true;
            x.UseMicrosoftSQLite(this._connectionString);
            x.UseNewtonsoftJsonSerializer();
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
      this._dbContext.Database.EnsureDeleted();
      this._dbContext.Dispose();
    }
  }
}
