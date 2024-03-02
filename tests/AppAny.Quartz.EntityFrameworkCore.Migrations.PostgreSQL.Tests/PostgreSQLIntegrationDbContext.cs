namespace AppAny.Quartz.EntityFrameworkCore.Migrations.PostgreSQL.Tests
{
  using Microsoft.EntityFrameworkCore;

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
