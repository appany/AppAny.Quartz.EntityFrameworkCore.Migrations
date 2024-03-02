namespace AppAny.Quartz.EntityFrameworkCore.Migrations.SqlServer.Tests
{
  using System;
  using System.Threading.Tasks;
  using Testcontainers.MsSql;

  public class DatabaseFixture : IAsyncLifetime
  {
    private readonly MsSqlContainer container;

    public DatabaseFixture()
    {
      int port = Random.Shared.Next(3400, 3499);

      this.container ??= new MsSqlBuilder()
        .WithPortBinding(port, MsSqlBuilder.MsSqlPort)
        .Build();
    }

    public string ConnectionString => this.container.GetConnectionString();

    public string ContainerId => $"{this.container.Id}";

    public static string Database => MsSqlBuilder.DefaultDatabase;

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
