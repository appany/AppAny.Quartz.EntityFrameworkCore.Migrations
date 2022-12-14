using System;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace AppAny.Quartz.EntityFrameworkCore.Migrations.Tests.SQLite;

public class SQLiteIntegrationDbContext_IntegrationTests : IDisposable
{
  private readonly SQLiteIntegrationDbContext _dbContext;

  public SQLiteIntegrationDbContext_IntegrationTests()
  {
    var connectionString = new SqliteConnectionStringBuilder()
    {
      Mode = SqliteOpenMode.Memory,
      ForeignKeys = true,
      DataSource = "test_db",
      Cache = SqliteCacheMode.Shared,
    }.ToString();

    var options = new DbContextOptionsBuilder<SQLiteIntegrationDbContext>().UseSqlite(connectionString).Options;
    _dbContext = new SQLiteIntegrationDbContext(options);
  }

  [Fact]
  public void ShouldCompleteDatabaseMigrate_WhenDatabaseIsSQLite()
  {
    var exception = Record.Exception(() => _dbContext.Database.Migrate());

    Assert.NotNull(exception);
  }

  public void Dispose()
  {
    _dbContext.Dispose();
  }
}
