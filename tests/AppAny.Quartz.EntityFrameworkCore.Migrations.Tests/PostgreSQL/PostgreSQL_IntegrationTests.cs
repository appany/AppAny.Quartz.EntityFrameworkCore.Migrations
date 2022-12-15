using System;
using AppAny.Quartz.EntityFrameworkCore.Migrations.Tests.Base;
using AppAny.Quartz.EntityFrameworkCore.Migrations.Tests.MySQL;
using AppAny.Quartz.EntityFrameworkCore.Migrations.Tests.SQLServer;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace AppAny.Quartz.EntityFrameworkCore.Migrations.Tests.PostgreSQL
{
  public class IntegrationTests
  {
    [Fact]
    public void PostgreSQL_CompleteMigration()
    {
      var options = new DbContextOptionsBuilder<PostgreSqlIntegrationDbContext>()
        .UseNpgsql(ConnectionStrings.PostgreSqlConnectionString)
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
      var options = new DbContextOptionsBuilder<MySqlIntegrationDbContext>()
        .UseMySql(ConnectionStrings.MySqlConnectionString, ServerVersion.AutoDetect(ConnectionStrings.MySqlConnectionString))
        .EnableSensitiveDataLogging()
        .LogTo(Console.WriteLine)
        .Options;

      using (var dbContext = new MySqlIntegrationDbContext(options))
      {
        dbContext.Database.Migrate();
      }
    }

    [Fact]
    public void SqlServer_CompleteMigration()
    {
      var options = new DbContextOptionsBuilder<SqlServerIntegrationDbContext>()
        .UseSqlServer(ConnectionStrings.SqlServerConnectionString)
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
