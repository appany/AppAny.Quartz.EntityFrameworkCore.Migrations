namespace AppAny.Quartz.EntityFrameworkCore.Migrations.SqlServer.Tests;

using System;
using System.Threading.Tasks;
using global::Quartz;
using Microsoft.EntityFrameworkCore;
using Xunit;

public class SqlServerIntegrationDbContextIntegrationTests : IClassFixture<DatabaseFixture>, IDisposable
{
  private readonly SqlServerIntegrationDbContext _dbContext;
  private readonly string _connectionString;

  public SqlServerIntegrationDbContextIntegrationTests(DatabaseFixture fixture)
  {
    this._connectionString = fixture.ConnectionString;

    var options = new DbContextOptionsBuilder<SqlServerIntegrationDbContext>()
      .UseSqlServer(this._connectionString)
      .Options;

    this._dbContext = new SqlServerIntegrationDbContext(options);
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
          x.Properties.Add("quartz.jobStore.tablePrefix", "[quartz].QRTZ_");
          x.PerformSchemaValidation = true;
          x.UseSqlServer(this._connectionString);
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
