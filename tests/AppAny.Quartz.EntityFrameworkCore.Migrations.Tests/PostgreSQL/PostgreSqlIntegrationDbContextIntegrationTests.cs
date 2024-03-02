using System;
using System.Threading.Tasks;
using AppAny.Quartz.EntityFrameworkCore.Migrations.Tests.Base;
using Microsoft.EntityFrameworkCore;
using Quartz;
using Xunit;

namespace AppAny.Quartz.EntityFrameworkCore.Migrations.Tests.PostgreSQL
{
  [Collection(nameof(TestFixture.ClientCollection))]
  public class PostgreSqlIntegrationDbContextIntegrationTests : IDisposable
  {
    private readonly PostgreSqlIntegrationDbContext _dbContext;
    private readonly string _connectionString;

    public PostgreSqlIntegrationDbContextIntegrationTests(TestFixture testFixture)
    {
      _connectionString = testFixture.PostgreSqlConnectionString;

      var options = new DbContextOptionsBuilder<PostgreSqlIntegrationDbContext>()
        .UseNpgsql(_connectionString)
        .Options;

      _dbContext = new PostgreSqlIntegrationDbContext(options);
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
            x.Properties.Add("quartz.jobStore.tablePrefix", "quartz.qrtz_");
            x.PerformSchemaValidation = true;
            x.UsePostgres(_connectionString);
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
      _dbContext.Database.EnsureDeleted();
      _dbContext.Dispose();
    }
  }
}
