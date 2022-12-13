using AppAny.Quartz.EntityFrameworkCore.Migrations.SQL;
using Microsoft.EntityFrameworkCore;

namespace AppAny.Quartz.EntityFrameworkCore.Migrations.Tests
{
  public class SqlIntegrationDbContext : DbContext
  {
    public SqlIntegrationDbContext(DbContextOptions<SqlIntegrationDbContext> options)
      : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.AddQuartz(builder => builder
        .UseSqlServer()
        .UsePrefix("QRTZ_")
        .UseNoSchema());
    }
  }
}
