using AppAny.Quartz.EntityFrameworkCore.Migrations.MySql;
using Microsoft.EntityFrameworkCore;

namespace AppAny.Quartz.EntityFrameworkCore.Migrations.Tests
{
  public class MysqlintegrationDbContext : DbContext
  {
    public MysqlintegrationDbContext(DbContextOptions<MysqlintegrationDbContext> options)
     : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.AddQuartz(builder => builder
        .UseMysql()
        .UsePrefix("qrtz_")
        .UseNoSchema());
    }
  }
}
