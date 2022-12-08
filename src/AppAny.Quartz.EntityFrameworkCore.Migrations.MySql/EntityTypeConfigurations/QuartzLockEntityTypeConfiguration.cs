using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppAny.Quartz.EntityFrameworkCore.Migrations;

namespace AppAny.Quartz.EntityFrameworkCore.Migrations.MySql
{
  public class QuartzLockEntityTypeConfiguration : IEntityTypeConfiguration<QuartzLock>
  {
    private readonly string? prefix;

    public QuartzLockEntityTypeConfiguration(string? prefix)
    {
      this.prefix = prefix;
    }

    public void Configure(EntityTypeBuilder<QuartzLock> builder)
    {
      builder.ToTable($"{prefix}locks");

      builder.HasKey(x => new { x.SchedulerName, x.LockName });

      builder.Property(x => x.SchedulerName)
        .HasColumnName("sched_name")
        .HasColumnType("varchar(120)")
        .IsRequired();

      builder.Property(x => x.LockName)
        .HasColumnName("lock_name")
        .HasColumnType("varchar(40)")
        .IsRequired();
    }
  }
}
