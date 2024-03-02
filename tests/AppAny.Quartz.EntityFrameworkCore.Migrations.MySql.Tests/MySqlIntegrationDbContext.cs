namespace AppAny.Quartz.EntityFrameworkCore.Migrations.MySql.Tests
{
  using AppAny.Quartz.EntityFrameworkCore.Migrations.MySql;
  using Microsoft.EntityFrameworkCore;

  public class MySqlIntegrationDbContext : DbContext
  {
    public MySqlIntegrationDbContext(DbContextOptions<MySqlIntegrationDbContext> options)
      : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.AddQuartz(builder => builder.UseMySql());
    }
  }
}
