using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace AppAny.Quartz.EntityFrameworkCore.Migrations.Tests
{
  public class IntegrationDbContextDesignTimeFactory : IDesignTimeDbContextFactory<PostgresIntegrationDbContext>
  {
    public PostgresIntegrationDbContext CreateDbContext(string[] args)
    {
      var options = new DbContextOptionsBuilder<PostgresIntegrationDbContext>().UseNpgsql(TestSetup.PostgresConnectionString).Options;

      return new PostgresIntegrationDbContext(options);
    }
  }
}
