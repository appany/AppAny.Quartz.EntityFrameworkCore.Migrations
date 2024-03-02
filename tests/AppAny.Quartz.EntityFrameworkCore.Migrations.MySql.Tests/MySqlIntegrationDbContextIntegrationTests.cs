namespace AppAny.Quartz.EntityFrameworkCore.Migrations.MySql.Tests
{
  using System;
  using System.Threading.Tasks;
  using global::Quartz;
  using Microsoft.EntityFrameworkCore;
  using Xunit;

  public class MySqlIntegrationDbContextIntegrationTests : IClassFixture<DatabaseFixture>, IDisposable
  {
    private readonly MySqlIntegrationDbContext _dbContext;
    private readonly string _connectionString;

    public MySqlIntegrationDbContextIntegrationTests(DatabaseFixture fixture)
    {
      this._connectionString = fixture.ConnectionString;

      var options = new DbContextOptionsBuilder<MySqlIntegrationDbContext>()
        .UseMySql(
          this._connectionString,
          ServerVersion.AutoDetect(fixture.ConnectionString))
        .Options;

      this._dbContext = new MySqlIntegrationDbContext(options);
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
            x.UseMySql(this._connectionString);
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
