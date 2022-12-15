using AppAny.Quartz.EntityFrameworkCore.Migrations.Tests.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace AppAny.Quartz.EntityFrameworkCore.Migrations.Tests.PostgreSQL
{
  public class IntegrationDbContextDesignTimeFactory : IDesignTimeDbContextFactory<PostgreSqlIntegrationDbContext>
  {
    public PostgreSqlIntegrationDbContext CreateDbContext(string[] args)
    {
      var options = new DbContextOptionsBuilder<PostgreSqlIntegrationDbContext>().UseNpgsql(ConnectionStrings.PostgreSqlConnectionString).Options;

      return new PostgreSqlIntegrationDbContext(options);
    }
  }
}
