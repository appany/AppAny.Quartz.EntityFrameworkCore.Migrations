# EntityFrameworkCore migrations for Quartz.NET

[![Nuget](https://img.shields.io/nuget/v/AppAny.Quartz.EntityFrameworkCore.Migrations.PostgreSQL.svg)](https://www.nuget.org/packages/AppAny.Quartz.EntityFrameworkCore.Migrations.PostgreSQL)
![Integration Tests](https://github.com/appany/AppAny.Quartz.EntityFrameworkCore.Migrations/workflows/Integration%20Tests/badge.svg)

This library handles schema migrations for Quartz.NET using EntityFrameworkCore migrations toolkit with one line of configuration

## Supported drivers

- [x] [PostgreSQL](https://www.nuget.org/packages/Npgsql.EntityFrameworkCore.PostgreSQL)

Feel free to contribute another drivers support

### Usage

```cs
# Configure DbContext
public class DatabaseContext : DbContext
{
  // ...

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    // Adds Quartz.NET PostgreSQL schema to EntityFrameworkCore
    modelBuilder.AddQuartz(builder => builder
      .UsePostgres()
      .UseSchema("quartz")
      .UseNoPrefix());
  }
}

# Configure Quartz.NET
storeOptions.UsePostgres(postgresOptions =>
{
  postgresOptions.UseDriverDelegate<PostgreSQLDelegate>();
  postgresOptions.ConnectionString = ...;
  postgresOptions.TablePrefix = ...;
});

```

Then add EntityFrameworkCore migration with Quartz.NET schema `dotnet ef migrations add AddQuartz` and:

- Add in-process migration using `databaseContext.Database.MigrateAsync()`
- Add out-of-process migration using `dotnet ef database update`
- Extract SQL for your migration tool `dotnet ef migrations script PreviousMigration AddQuartz`
