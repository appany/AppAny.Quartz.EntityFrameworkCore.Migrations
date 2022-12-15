using AppAny.Quartz.EntityFrameworkCore.Migrations.MySql;
using Microsoft.EntityFrameworkCore;

namespace AppAny.Quartz.EntityFrameworkCore.Migrations.Tests
{
  public class MySqlintegrationDbContext : DbContext
  {
    public MySqlintegrationDbContext(DbContextOptions<MySqlintegrationDbContext> options)
      : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.AddQuartz(builder => builder
        .UseMySql()
        .UseNoSchema());
    }
  }
}
