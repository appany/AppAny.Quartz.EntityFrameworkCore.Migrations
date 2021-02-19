# 💥 EntityFrameworkCore migrations for Quartz.NET 💥

[![License](https://img.shields.io/github/license/appany/AppAny.HotChocolate.FluentValidation.svg)](https://github.com/appany/AppAny.HotChocolate.FluentValidation/blob/main/LICENSE)
[![Nuget](https://img.shields.io/nuget/v/AppAny.Quartz.EntityFrameworkCore.Migrations.PostgreSQL.svg)](https://www.nuget.org/packages/AppAny.Quartz.EntityFrameworkCore.Migrations.PostgreSQL)
[![Downloads](https://img.shields.io/nuget/dt/AppAny.Quartz.EntityFrameworkCore.Migrations)](https://www.nuget.org/packages/AppAny.Quartz.EntityFrameworkCore.Migrations)
![Tests](https://github.com/appany/AppAny.Quartz.EntityFrameworkCore.Migrations/workflows/Tests/badge.svg)
[![codecov](https://codecov.io/gh/appany/AppAny.Quartz.EntityFrameworkCore.Migrations/branch/main/graph/badge.svg?token=589PU3Y1S9)](https://codecov.io/gh/appany/AppAny.Quartz.EntityFrameworkCore.Migrations)

⚡️ This library handles schema migrations for Quartz.NET using EntityFrameworkCore migrations toolkit with one line of configuration ⚡️

## 🔧 Installation 🔧

```bash
$> dotnet add package AppAny.Quartz.EntityFrameworkCore.Migrations.PostgreSQL
```

## 💡 Supported drivers 💡

- [x] [PostgreSQL](https://www.nuget.org/packages/Npgsql.EntityFrameworkCore.PostgreSQL)

🚧 Feel free to **create as issue** for driver support 🚧

## 🎨 Usage 🎨

✅ Configure `DbContext`
```cs
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
```

✅ Configure `Quartz.NET`
```cs
storeOptions.UsePostgres(postgresOptions =>
{
  postgresOptions.UseDriverDelegate<PostgreSQLDelegate>();
  postgresOptions.ConnectionString = ...;
  postgresOptions.TablePrefix = ...;
});
```

✅ Add EntityFrameworkCore migration with Quartz.NET schema `dotnet ef migrations add AddQuartz` and:

🚩 Add in-process migration using `databaseContext.Database.MigrateAsync()`

🚩 Add out-of-process migration using `dotnet ef database update`

🚩 Extract SQL for your migration tool `dotnet ef migrations script PreviousMigration AddQuartz`
