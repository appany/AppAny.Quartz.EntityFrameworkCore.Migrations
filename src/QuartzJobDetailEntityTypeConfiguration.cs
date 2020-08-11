using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AppAny.Quartz.EntityFrameworkCore.Migrations.PostgreSQL
{
	public class QuartzJobDetailEntityTypeConfiguration : IEntityTypeConfiguration<QuartzJobDetail>
	{
		private readonly string prefix;
		private readonly string? schema;

		public QuartzJobDetailEntityTypeConfiguration(string prefix, string? schema)
		{
			this.prefix = prefix;
			this.schema = schema;
		}

		public void Configure(EntityTypeBuilder<QuartzJobDetail> builder)
		{
			builder.ToTable($"{prefix}job_details", schema);

			builder.HasKey(x => new {x.SchedulerName, x.JobName, x.JobGroup});

			builder.Property(x => x.SchedulerName)
				.HasColumnName("sched_name")
				.HasColumnType("text")
				.IsRequired();

			builder.Property(x => x.JobName)
				.HasColumnName("job_name")
				.HasColumnType("text")
				.IsRequired();

			builder.Property(x => x.JobGroup)
				.HasColumnName("job_group")
				.HasColumnType("text")
				.IsRequired();

			builder.Property(x => x.Description)
				.HasColumnName("description")
				.HasColumnType("text")
				.IsRequired();

			builder.Property(x => x.JobClassName)
				.HasColumnName("job_class_name")
				.HasColumnType("text")
				.IsRequired();

			builder.Property(x => x.IsDurable)
				.HasColumnName("is_durable")
				.HasColumnType("bool")
				.IsRequired();

			builder.Property(x => x.IsNonConcurrent)
				.HasColumnName("is_nonconcurrent")
				.HasColumnType("bool")
				.IsRequired();

			builder.Property(x => x.IsUpdateData)
				.HasColumnName("is_update_data")
				.HasColumnType("bool")
				.IsRequired();

			builder.Property(x => x.RequestsRecovery)
				.HasColumnName("requests_recovery")
				.HasColumnType("bool")
				.IsRequired();

			builder.Property(x => x.JobData)
				.HasColumnName("job_data")
				.HasColumnType("bytea");

			builder.HasIndex(x => x.RequestsRecovery)
				// TODO: Prefix?
				.HasDatabaseName("idx_qrtz_j_req_recovery");
		}
	}
}