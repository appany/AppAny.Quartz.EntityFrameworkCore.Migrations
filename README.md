# 💥 EntityFrameworkCore migrations for Quartz.NET 💥

[![License](https://img.shields.io/github/license/appany/AppAny.HotChocolate.FluentValidation.svg)](https://github.com/appany/AppAny.HotChocolate.FluentValidation/blob/main/LICENSE)
[![Nuget](https://img.shields.io/nuget/v/AppAny.Quartz.EntityFrameworkCore.Migrations.PostgreSQL.svg)](https://www.nuget.org/packages/AppAny.Quartz.EntityFrameworkCore.Migrations.PostgreSQL)
[![Downloads](https://img.shields.io/nuget/dt/AppAny.Quartz.EntityFrameworkCore.Migrations)](https://www.nuget.org/packages/AppAny.Quartz.EntityFrameworkCore.Migrations)
![Tests](https://github.com/appany/AppAny.Quartz.EntityFrameworkCore.Migrations/workflows/Tests/badge.svg)
[![codecov](https://codecov.io/gh/appany/AppAny.Quartz.EntityFrameworkCore.Migrations/branch/main/graph/badge.svg?token=589PU3Y1S9)](https://codecov.io/gh/appany/AppAny.Quartz.EntityFrameworkCore.Migrations)

⚡️ This library handles schema creation and migrations for Quartz.NET using EntityFrameworkCore migrations toolkit with one line of configuration ⚡️

## 💡 Supported drivers 💡

- [x] [MySQL](https://www.nuget.org/packages/Pomelo.EntityFrameworkCore.MySql)
- [x] [PostgreSQL](https://www.nuget.org/packages/Npgsql.EntityFrameworkCore.PostgreSQL)
- [x] [SQLite](https://www.nuget.org/packages/Microsoft.EntityFrameworkCore.Sqlite)
- [x] [SQLServer](https://www.nuget.org/packages/Microsoft.EntityFrameworkCore.SqlServer)

🚧 Feel free to **create as issue** for driver support 🚧

## 🔧 Installation 🔧

### MySql

```bash
dotnet add package AppAny.Quartz.EntityFrameworkCore.Migrations.MySql
```

### PostgreSQL

```bash
dotnet add package AppAny.Quartz.EntityFrameworkCore.Migrations.PostgreSQL
```

### SQLite

```bash
dotnet add package AppAny.Quartz.EntityFrameworkCore.Migrations.SQLite
```

### SqlServer

```bash
dotnet add package AppAny.Quartz.EntityFrameworkCore.Migrations.SqlServer
```

## Quartz Versions

The schema includes the Quartz 4.0, 4.2 and 4.3 additions and remains compatible
with Quartz.NET 3.21.0 and 4.0.1. All four database providers include:

- The Quartz 4.0 trigger columns and `QRTZ_PAUSED_JOB_GRPS` table.
- The Quartz 4.2 continuation columns, `QRTZ_EXECUTION_HISTORY` and
  `QRTZ_MISFIRE_HISTORY` tables, including retry metadata and retention indexes.
- The Quartz 4.3 overlap policy, progress and pause reason columns, execution log
  and misfire reason.

The indexes match the Quartz.NET 4.0.1 table scripts for every provider. Upgrading from 0.6.1 or earlier
produces a migration that drops the legacy indexes, renames two of them and creates the new ones
(including a reshaped `IDX_QRTZ_T_NFT_ST`). All indexes are non-unique, but on large tables apply it in a
maintenance window. On MySQL, stop the schedulers while it runs: Quartz references `IDX_QRTZ_T_NFT_ST` by
name (`FORCE INDEX`) and the index is briefly missing. The Oracle `MySql.EntityFrameworkCore` provider
(net10.0) ignores the `PRIORITY DESC` column direction of `IDX_QRTZ_T_NFT_ST`.

The history tables are created with the rest of the schema. Quartz only uses them
when `UsePersistentStore(store => store.UseExecutionHistory())` is enabled. They
have no foreign keys so history remains available after a job or trigger is deleted.

For an existing database, scaffold and apply a new EF Core migration before
upgrading the scheduler, for example `dotnet ef migrations add UpgradeQuartz43`
followed by `dotnet ef database update`. Nullable columns and history retry defaults
preserve existing rows; no version-specific model configuration is required.

With clustered schedulers, migrate first and upgrade every node before using
continuations or overlap policies.

The packages do not reference Quartz.NET directly. The .NET 10 test projects use
Quartz 4.3.0 by default and CI tests all four versions. To select a version:

```bash
dotnet test --framework net10.0 -p:QuartzVersion=4.2.0
dotnet test --framework net10.0 -p:QuartzVersion=4.3.0
```

## 🎨 Usage 🎨

✅ Configure the `DbContext` that will hold the Quartz.NET tables
```cs
public class DatabaseContext : DbContext
{
  // ...

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    // Prefix and schema can be passed as parameters

    // Adds Quartz.NET MySql schema to EntityFrameworkCore
    modelBuilder.AddQuartz(builder => builder.UseMySql());

    // Adds Quartz.NET PostgreSQL schema to EntityFrameworkCore
    modelBuilder.AddQuartz(builder => builder.UsePostgreSql());

    // Adds Quartz.NET SQLite schema to EntityFrameworkCore
    modelBuilder.AddQuartz(builder => builder.UseSQLite());

    // Adds Quartz.NET SqlServer schema to EntityFrameworkCore
    modelBuilder.AddQuartz(builder => builder.UseSqlServer());
  }
}
```

✅ (Optional) [ASP.NET Core Integration](https://www.quartz-scheduler.net/documentation/quartz-3.x/packages/aspnet-core-integration.html#using) Configuration

```csharp
// Startup.cs
public void ConfigureServices(IServiceCollection services)
{
    services.AddQuartz(q =>
    {
        q.UsePersistentStore(c =>
        {
            // Use for MySQL database
            c.UseMySql(mysqlOptions =>
            {
                  mysqlOptions.UseDriverDelegate<MySQLDelegate>();
                  mysqlOptions.ConnectionString = ...;
                  mysqlOptions.TablePrefix = ...;
            });

            // Use for PostgresSQL database
            c.UsePostgres(postgresOptions =>
            {
                  postgresOptions.UseDriverDelegate<PostgreSQLDelegate>();
                  postgresOptions.ConnectionString = ...;
                  postgresOptions.TablePrefix = ...;
            });

            // Use for SQLite database
            c.UseSQLite(sqlLiteOptions =>
            {
                  sqlLiteOptions.UseDriverDelegate<SQLiteDelegate>();
                  sqlLiteOptions.ConnectionString = ...;
                  sqlLiteOptions.TablePrefix = ...;
            });

            // Use for SqlServer database
            c.UseSqlServer(sqlServerOptions =>
            {
                  sqlServerOptions.UseDriverDelegate<SqlServerDelegate>();
                  sqlServerOptions.ConnectionString = ...;
                  sqlServerOptions.TablePrefix = ...;
            });
        });
    });
}

```

## 🚩 Finish 🚩

✅ Add EntityFrameworkCore migration with Quartz.NET schema `dotnet ef migrations add AddQuartz` and then

* 🚩 Add in-process migration using `databaseContext.Database.MigrateAsync()`

* 🚩 Add out-of-process migration using `dotnet ef database update`

* 🚩 Extract SQL for your migration tool `dotnet ef migrations script PreviousMigration AddQuartz`
