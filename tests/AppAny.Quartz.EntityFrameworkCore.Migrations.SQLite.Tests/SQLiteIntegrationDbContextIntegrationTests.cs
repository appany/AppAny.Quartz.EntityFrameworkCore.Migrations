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

    public void Dispose()
    {
      this._dbContext.Database.EnsureDeleted();
      this._dbContext.Dispose();
    }
  }
}
