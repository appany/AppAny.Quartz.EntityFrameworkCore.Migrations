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

#if NET10_0_OR_GREATER
      // Oracle MySQL provider uses UseMySQL (capital SQL) and doesn't require ServerVersion parameter
      var options = new DbContextOptionsBuilder<MySqlIntegrationDbContext>()
        .UseMySQL(this._connectionString)
        .Options;
#else
      // Pomelo provider uses UseMySql and requires ServerVersion parameter (NET8_0)
      var options = new DbContextOptionsBuilder<MySqlIntegrationDbContext>()
        .UseMySql(
          this._connectionString,
          ServerVersion.AutoDetect(fixture.ConnectionString))
        .Options;
#endif

      this._dbContext = new MySqlIntegrationDbContext(options);
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
            x.UseMySql(this._connectionString);
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
}
