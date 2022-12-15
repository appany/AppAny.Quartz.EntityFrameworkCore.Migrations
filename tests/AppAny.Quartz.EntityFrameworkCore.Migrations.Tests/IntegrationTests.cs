using System;
using Xunit;
using Microsoft.EntityFrameworkCore;

namespace AppAny.Quartz.EntityFrameworkCore.Migrations.Tests
{
  public class IntegrationTests
  {
    [Fact]
    public void PostgreSQL_CompleteMigration()
    {
      var options = new DbContextOptionsBuilder<PostgreSqlIntegrationDbContext>()
        .UseNpgsql(TestSetup.PostgreSqlConnectionString)
        .EnableSensitiveDataLogging()
        .LogTo(Console.WriteLine)
        .Options;

      using (var dbContext = new PostgreSqlIntegrationDbContext(options))
      {
        dbContext.Database.Migrate();
      }
    }

    [Fact]
    public void MySQL_CompleteMigration()
    {
      var options = new DbContextOptionsBuilder<MySqlintegrationDbContext>()
        .UseMySql(TestSetup.MySqlConnectionString, ServerVersion.AutoDetect(TestSetup.MySqlConnectionString))
        .EnableSensitiveDataLogging()
        .LogTo(Console.WriteLine)
        .Options;

      using (var dbContext = new MySqlintegrationDbContext(options))
      {
        dbContext.Database.Migrate();
      }
    }

    [Fact]
    public void SqlServer_CompleteMigration()
    {
      var options = new DbContextOptionsBuilder<SqlServerIntegrationDbContext>()
        .UseSqlServer(TestSetup.SqlServerConnectionString)
        .EnableSensitiveDataLogging()
        .LogTo(Console.WriteLine)
        .Options;

      using (var dbContext = new SqlServerIntegrationDbContext(options))
      {
        dbContext.Database.Migrate();
      }
    }
  }
}
