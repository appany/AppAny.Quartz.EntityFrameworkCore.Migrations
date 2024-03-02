namespace AppAny.Quartz.EntityFrameworkCore.Migrations.PostgreSQL.Tests
{
  using System;
  using System.Threading.Tasks;
  using global::Quartz;
  using Microsoft.EntityFrameworkCore;
  using Xunit;

  public class PostgreSqlIntegrationDbContextIntegrationTests : IClassFixture<DatabaseFixture>, IDisposable
  {
    private readonly PostgreSqlIntegrationDbContext _dbContext;
    private readonly string _connectionString;

    public PostgreSqlIntegrationDbContextIntegrationTests(DatabaseFixture fixture)
    {
      this._connectionString = fixture.ConnectionString;

      var options = new DbContextOptionsBuilder<PostgreSqlIntegrationDbContext>()
        .UseNpgsql(this._connectionString)
        .Options;

      this._dbContext = new PostgreSqlIntegrationDbContext(options);
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
            x.Properties.Add("quartz.jobStore.tablePrefix", "quartz.qrtz_");
            x.PerformSchemaValidation = true;
            x.UsePostgres(this._connectionString);
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
      //this._dbContext.Database.EnsureDeleted();
      this._dbContext.Dispose();
    }
  }
}
