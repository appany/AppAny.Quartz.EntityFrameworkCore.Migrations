using System.Collections.Generic;
using System.Threading.Tasks;
using TestEnvironment.Docker;
using TestEnvironment.Docker.Containers.Mssql;
using TestEnvironment.Docker.Containers.Postgres;
using Xunit;

namespace AppAny.Quartz.EntityFrameworkCore.Migrations.Tests.Base
{
  public class TestFixture : IAsyncLifetime
  {
    private const string SQL_SA_PASSWORD = "Admin1234.";
    private const string MYSQL_ROOT_PASSWORD = "password";

    private readonly IDockerEnvironment _dockerEnvironment = new DockerEnvironmentBuilder()
      .AddPostgresContainer(
        p => p with
        {
          Name = "postgres"
        })
      .AddContainer(
        p => p with
        {
          ImageName = "mysql",
          Name = "mysql",
          EnvironmentVariables = new Dictionary<string, string>
          {
            { "MYSQL_ROOT_PASSWORD", MYSQL_ROOT_PASSWORD }
          },
          ExposedPorts = new List<ushort>
          {
            3306
          }
        })
      .AddMssqlContainer(
        p => p with
        {
          Name = "sqlserver",
          ImageName = "mcr.microsoft.com/azure-sql-edge",
          SAPassword = SQL_SA_PASSWORD
        })
      .Build();

    public string PostgreSqlConnectionString { get; private set; } = null!;
    public string SqlServerConnectionString { get; private set; } = null!;
    public string MySqlConnectionString { get; private set; } = null!;

    public async Task InitializeAsync()
    {
      await _dockerEnvironment.UpAsync();

      // set connection string for integration tests
      PostgreSqlConnectionString =
        _dockerEnvironment.GetContainer<PostgresContainer>("postgres")!.GetConnectionString();

      MySqlConnectionString =
        $"server=localhost; port={_dockerEnvironment.GetContainer("mysql")!.Ports![3306]}; database=test; uid=root; pwd=password;";

      SqlServerConnectionString =
        $"{_dockerEnvironment.GetContainer<MssqlContainer>("sqlserver")!.GetConnectionString()}Initial catalog=test";
    }

    public async Task DisposeAsync()
    {
      await _dockerEnvironment.DownAsync();
      await _dockerEnvironment.DisposeAsync();
    }

    [CollectionDefinition(nameof(ClientCollection))]
    public class ClientCollection : ICollectionFixture<TestFixture>
    {
    }
  }
}
