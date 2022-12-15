using AppAny.Quartz.EntityFrameworkCore.Migrations.SQLite;
using Microsoft.EntityFrameworkCore;

namespace AppAny.Quartz.EntityFrameworkCore.Migrations.Tests.SQLite
{
  public class SQLiteIntegrationDbContext : DbContext
  {
    public SQLiteIntegrationDbContext(DbContextOptions<SQLiteIntegrationDbContext> options)
      : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.AddQuartz(builder => builder.UseSqlite());
    }
  }
}
