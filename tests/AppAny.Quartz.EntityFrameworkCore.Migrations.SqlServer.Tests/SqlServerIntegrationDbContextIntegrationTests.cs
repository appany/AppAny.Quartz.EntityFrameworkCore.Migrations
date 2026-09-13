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
          x.ConfigureStore(options =>
          {
            options.TablePrefix = "[quartz].QRTZ_";
            options.SchemaProvisioning = SchemaProvisioning.Validate;
          });
#else
          x.Properties.Add("quartz.jobStore.tablePrefix", "[quartz].QRTZ_");
          x.PerformSchemaValidation = true;
#endif
          x.UseSqlServer(this._connectionString);
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
    //this._dbContext.Database.EnsureDeleted();
    this._dbContext.Dispose();
  }
}
