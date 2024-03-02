namespace AppAny.Quartz.EntityFrameworkCore.Migrations.SqlServer.Tests
{
  using AppAny.Quartz.EntityFrameworkCore.Migrations.SqlServer;
  using Microsoft.EntityFrameworkCore;

  public class SqlServerIntegrationDbContext : DbContext
  {
    public SqlServerIntegrationDbContext(DbContextOptions<SqlServerIntegrationDbContext> options)
      : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.AddQuartz(builder => builder.UseSqlServer());
    }
  }
}
