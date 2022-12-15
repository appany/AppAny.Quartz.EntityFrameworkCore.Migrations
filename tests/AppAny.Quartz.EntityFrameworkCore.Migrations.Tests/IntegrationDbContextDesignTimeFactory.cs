using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace AppAny.Quartz.EntityFrameworkCore.Migrations.Tests
{
  public class IntegrationDbContextDesignTimeFactory : IDesignTimeDbContextFactory<PostgreSqlIntegrationDbContext>
  {
    public PostgreSqlIntegrationDbContext CreateDbContext(string[] args)
    {
      var options = new DbContextOptionsBuilder<PostgreSqlIntegrationDbContext>().UseNpgsql(TestSetup.PostgreSqlConnectionString).Options;

      return new PostgreSqlIntegrationDbContext(options);
    }
  }
}
