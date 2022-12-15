using AppAny.Quartz.EntityFrameworkCore.Migrations.SqlServer;
using Microsoft.EntityFrameworkCore;

namespace AppAny.Quartz.EntityFrameworkCore.Migrations.Tests
{
  public class SqlServerIntegrationDbContext : DbContext
  {
    public SqlServerIntegrationDbContext(DbContextOptions<SqlServerIntegrationDbContext> options)
      : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.AddQuartz(builder => builder
        .UseSqlServer()
        .UseNoSchema());
    }
  }
}
