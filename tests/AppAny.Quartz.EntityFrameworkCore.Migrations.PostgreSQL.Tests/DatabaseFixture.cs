namespace AppAny.Quartz.EntityFrameworkCore.Migrations.PostgreSQL.Tests
{
  using System;
  using System.Threading.Tasks;
  using Testcontainers.PostgreSql;

  public class DatabaseFixture : IAsyncLifetime
  {
    private readonly PostgreSqlContainer container;

    public DatabaseFixture()
    {
      int port = Random.Shared.Next(3400, 3499);

      this.container ??= new PostgreSqlBuilder()
        .WithPortBinding(port, PostgreSqlBuilder.PostgreSqlPort)
        .Build();
    }

    public string ConnectionString => this.container.GetConnectionString();

    public string ContainerId => $"{this.container.Id}";

    public static string Database => PostgreSqlBuilder.DefaultDatabase;

    /// <inheritdoc />
    public Task InitializeAsync()
    {
      return this.container.StartAsync();
    }

    /// <inheritdoc />
    public Task DisposeAsync()
    {
      return this.container.DisposeAsync().AsTask();
    }
  }
}
