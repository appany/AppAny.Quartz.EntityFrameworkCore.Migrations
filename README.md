# PostgreSQL migrations using EntityFrameworkCore for Quartz.NET Job Scheduler

[![Nuget](https://img.shields.io/nuget/v/AppAny.Quartz.EntityFrameworkCore.Migrations.PostgreSQL.svg)](https://www.nuget.org/packages/AppAny.Quartz.EntityFrameworkCore.Migrations.PostgreSQL)

This library handles schema migrations for Quartz.NET using EntityFrameworkCore migrations toolkit with one line of configuration

## Usage

```cs
public class DatabaseContext : DbContext
{
  public DatabaseContext(DbContextOptions<DatabaseContext> options)
  : base(options)
  {
  }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    // Adds Quartz.NET schema to EntityFrameworkCore Model
    modelBuilder.AddQuartzPostgres(builder => builder
      .UseSchema("quartz")
      .UseNoPrefix());
  }
}
```

Then add EntityFrameworkCore with Quartz.NET schema migration `dotnet ef migrations add AddQuartz` and:

- Add in-process migration using `databaseContext.Database.MigrateAsync()`
- Add out-of-process migration using `dotnet ef database update`
- Extract SQL for your migration tool `dotnet ef migrations script PreviousMigration AddQuartz`
