using AppAny.Quartz.EntityFrameworkCore.Migrations.PostgreSQL;
using Microsoft.EntityFrameworkCore;

namespace AppAny.Quartz.EntityFrameworkCore.Migrations.Tests
{
  public class PostgresIntegrationDbContext : DbContext
  {
    public PostgresIntegrationDbContext(DbContextOptions<PostgresIntegrationDbContext> options)
      : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.AddQuartz(builder => builder
        .UsePostgres()
        .UseSchema("quartz")
        .UseNoPrefix());
    }
  }
}
