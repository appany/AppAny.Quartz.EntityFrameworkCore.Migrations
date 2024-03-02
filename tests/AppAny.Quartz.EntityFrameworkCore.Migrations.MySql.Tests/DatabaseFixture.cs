namespace AppAny.Quartz.EntityFrameworkCore.Migrations.MySql.Tests
{
  using System;
  using System.Threading.Tasks;
  using Testcontainers.MySql;

  public class DatabaseFixture : IAsyncLifetime
  {
    private readonly MySqlContainer container;

    public DatabaseFixture()
    {
      int port = Random.Shared.Next(3400, 3499);

      this.container ??= new MySqlBuilder()
        .WithPortBinding(port, MySqlBuilder.MySqlPort)
        .Build();
    }

    public string ConnectionString => this.container.GetConnectionString();

    public string ContainerId => $"{this.container.Id}";

    public static string Database => MySqlBuilder.DefaultDatabase;

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
