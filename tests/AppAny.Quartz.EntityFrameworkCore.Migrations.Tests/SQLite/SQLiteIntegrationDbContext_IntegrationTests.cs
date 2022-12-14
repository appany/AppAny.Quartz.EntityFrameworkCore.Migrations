using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace AppAny.Quartz.EntityFrameworkCore.Migrations.Tests.SQLite;

public class SQLiteIntegrationDbContext_IntegrationTests
{
  private readonly string _connectionString;

  public SQLiteIntegrationDbContext_IntegrationTests()
  {
    _connectionString = new SqliteConnectionStringBuilder()
    {
      Mode = SqliteOpenMode.Memory,
      ForeignKeys = true,
      DataSource = "test_db",
      Cache = SqliteCacheMode.Shared,
    }.ToString();
  }

  [Fact]
  public void SQLite_CompleteMigration()
  {
    var options = new DbContextOptionsBuilder<SQLiteIntegrationDbContext>().UseSqlite(_connectionString).Options;

    using (var dbContext = new SQLiteIntegrationDbContext(options))
    {
      dbContext.Database.Migrate();
    }
  }
}
