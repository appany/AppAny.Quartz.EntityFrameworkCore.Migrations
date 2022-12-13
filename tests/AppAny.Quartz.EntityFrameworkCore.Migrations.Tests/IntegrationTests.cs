using Xunit;
using Microsoft.EntityFrameworkCore;

namespace AppAny.Quartz.EntityFrameworkCore.Migrations.Tests
{
  using System;

  public class IntegrationTests
  {
    [Fact]
    public void Postgres_CompleteMigration()
    {
      var options = new DbContextOptionsBuilder<PostgresIntegrationDbContext>()
        .UseNpgsql(TestSetup.PostgresConnectionString)
        .Options;

      using (var dbContext = new PostgresIntegrationDbContext(options))
      {
        dbContext.Database.Migrate();
      }
    }

    [Fact]
    public void MySQL_CompleteMigration()
    {
      var options = new DbContextOptionsBuilder<MysqlintegrationDbContext>().UseMySql(
          TestSetup.MysqlConnectionString,
          ServerVersion.AutoDetect(TestSetup.MysqlConnectionString))
        .Options;

      using (var dbContext = new MysqlintegrationDbContext(options))
      {
        dbContext.Database.Migrate();
      }
    }

    [Fact]
    public void Sql_CompleteMigration()
    {
      var options = new DbContextOptionsBuilder<SqlIntegrationDbContext>()
        .UseSqlServer(TestSetup.SqlConnectionString)
        .EnableSensitiveDataLogging()
        .LogTo(Console.WriteLine)
        .Options;

      using (var dbContext = new SqlIntegrationDbContext(options))
      {
        dbContext.Database.Migrate();
      }
    }
  }
}
