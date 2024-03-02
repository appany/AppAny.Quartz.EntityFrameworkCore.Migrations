using System;
using System.Threading.Tasks;
using AppAny.Quartz.EntityFrameworkCore.Migrations.Tests.Base;
using Microsoft.EntityFrameworkCore;
using Quartz;
using Xunit;

namespace AppAny.Quartz.EntityFrameworkCore.Migrations.Tests.MySQL
{
  [Collection(nameof(TestFixture.ClientCollection))]
  public class MySqlIntegrationDbContextIntegrationTests : IDisposable
  {
    private readonly MySqlIntegrationDbContext _dbContext;
    private readonly string _connectionString;

    public MySqlIntegrationDbContextIntegrationTests(TestFixture testFixture)
    {
      _connectionString = testFixture.MySqlConnectionString;

      var options = new DbContextOptionsBuilder<MySqlIntegrationDbContext>()
        .UseMySql(
          _connectionString,
          ServerVersion.AutoDetect(testFixture.MySqlConnectionString))
        .Options;

      _dbContext = new MySqlIntegrationDbContext(options);
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
            x.UseMySql(_connectionString);
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
