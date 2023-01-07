namespace AppAny.Quartz.EntityFrameworkCore.Migrations.Tests.SQLServer;

using System;
using System.Threading.Tasks;
using AppAny.Quartz.EntityFrameworkCore.Migrations.Tests.Base;
using global::Quartz;
using Microsoft.EntityFrameworkCore;
using Xunit;

[Collection(nameof(TestFixture.ClientCollection))]
public class SqlServerIntegrationDbContextIntegrationTests : IDisposable
{
  private readonly SqlServerIntegrationDbContext _dbContext;
  private readonly string _connectionString;

  public SqlServerIntegrationDbContextIntegrationTests(TestFixture testFixture)
  {
    _connectionString = testFixture.SqlServerConnectionString;

    var options = new DbContextOptionsBuilder<SqlServerIntegrationDbContext>()
      .UseSqlServer(_connectionString)
      .Options;

    _dbContext = new SqlServerIntegrationDbContext(options);
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
          x.Properties.Add("quartz.jobStore.tablePrefix", "[quartz].QRTZ_");
          x.PerformSchemaValidation = true;
          x.UseSqlServer(_connectionString);
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
