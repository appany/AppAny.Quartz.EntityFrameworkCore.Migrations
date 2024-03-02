namespace AppAny.Quartz.EntityFrameworkCore.Migrations.SQLite.Tests
{
  using AppAny.Quartz.EntityFrameworkCore.Migrations.SQLite;
  using Microsoft.EntityFrameworkCore;

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
