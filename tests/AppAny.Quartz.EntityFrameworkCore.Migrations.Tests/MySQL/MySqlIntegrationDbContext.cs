using AppAny.Quartz.EntityFrameworkCore.Migrations.MySql;
using Microsoft.EntityFrameworkCore;

namespace AppAny.Quartz.EntityFrameworkCore.Migrations.Tests.MySQL
{
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
