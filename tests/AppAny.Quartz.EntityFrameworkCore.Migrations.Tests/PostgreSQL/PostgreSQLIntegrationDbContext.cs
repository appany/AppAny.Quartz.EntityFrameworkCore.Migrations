using AppAny.Quartz.EntityFrameworkCore.Migrations.PostgreSQL;
using Microsoft.EntityFrameworkCore;

namespace AppAny.Quartz.EntityFrameworkCore.Migrations.Tests.PostgreSQL
{
  public class PostgreSqlIntegrationDbContext : DbContext
  {
    public PostgreSqlIntegrationDbContext(DbContextOptions<PostgreSqlIntegrationDbContext> options)
      : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.AddQuartz(builder => builder.UsePostgreSql());
    }
  }
}
