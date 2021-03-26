using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace AppAny.Quartz.EntityFrameworkCore.Migrations.Tests
{
  public class IntegrationDbContextDesignTimeFactory : IDesignTimeDbContextFactory<IntegrationDbContext>
  {
    public IntegrationDbContext CreateDbContext(string[] args)
    {
      var options = new DbContextOptionsBuilder<IntegrationDbContext>().UseNpgsql(TestSetup.ConnectionString).Options;

      return new IntegrationDbContext(options);
    }
  }
}
