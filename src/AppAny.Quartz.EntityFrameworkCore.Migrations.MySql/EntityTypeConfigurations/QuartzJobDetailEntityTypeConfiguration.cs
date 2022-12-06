using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppAny.Quartz.EntityFrameworkCore.Migrations.Quartz;

namespace AppAny.Quartz.EntityFrameworkCore.Migrations.MySql.EntityTypeConfigurations
{
  public class QuartzJobDetailEntityTypeConfiguration : IEntityTypeConfiguration<QuartzJobDetail>
  {
    private readonly string? prefix;

    public QuartzJobDetailEntityTypeConfiguration(string? prefix)
    {
      this.prefix = prefix;
    }

    public void Configure(EntityTypeBuilder<QuartzJobDetail> builder)
    {
      builder.ToTable($"{prefix}job_details");

      builder.HasKey(x => new { x.SchedulerName, x.JobName, x.JobGroup });

      builder.Property(x => x.SchedulerName)
        .HasColumnName("sched_name")
        .HasColumnType("varchar(120)")
        .IsRequired();

      builder.Property(x => x.JobName)
        .HasColumnName("job_name")
        .HasColumnType("varchar(200)")
        .IsRequired();

      builder.Property(x => x.JobGroup)
        .HasColumnName("job_group")
        .HasColumnType("varchar(200)")
        .IsRequired();

      builder.Property(x => x.Description)
        .HasColumnName("description")
        .HasColumnType("varchar(250)");

      builder.Property(x => x.JobClassName)
        .HasColumnName("job_class_name")
        .HasColumnType("varchar(250)")
        .IsRequired();

      builder.Property(x => x.IsDurable)
        .HasColumnName("is_durable")
        .HasColumnType("tinyint(1)")
        .IsRequired();

      builder.Property(x => x.IsNonConcurrent)
        .HasColumnName("is_nonconcurrent")
        .HasColumnType("tinyint(1)")
        .IsRequired();

      builder.Property(x => x.IsUpdateData)
        .HasColumnName("is_update_data")
        .HasColumnType("tinyint(1)")
        .IsRequired();

      builder.Property(x => x.RequestsRecovery)
        .HasColumnName("requests_recovery")
        .HasColumnType("tinyint(1)")
        .IsRequired();

      builder.Property(x => x.JobData)
        .HasColumnName("job_data")
        .HasColumnType("blob");

      builder.HasIndex(x => x.RequestsRecovery)
        .HasDatabaseName($"idx_{prefix}j_req_recovery");
    }
  }
}
