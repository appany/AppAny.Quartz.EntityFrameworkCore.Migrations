using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AppAny.Quartz.EntityFrameworkCore.Migrations.PostgreSQL
{
	public class QuartzFiredTriggerEntityTypeConfiguration : IEntityTypeConfiguration<QuartzFiredTrigger>
	{
		private readonly string prefix;
		private readonly string? schema;

		public QuartzFiredTriggerEntityTypeConfiguration(string prefix, string? schema)
		{
			this.prefix = prefix;
			this.schema = schema;
		}

		public void Configure(EntityTypeBuilder<QuartzFiredTrigger> builder)
		{
			builder.ToTable("fired_triggers", schema);

			builder.HasKey(x => new {x.SchedulerName, x.EntryId});

			builder.Property(x => x.SchedulerName)
				.HasColumnName($"{prefix}sched_name")
				.HasColumnType("text")
				.IsRequired();

			builder.Property(x => x.EntryId)
				.HasColumnName($"{prefix}entry_id")
				.HasColumnType("text")
				.IsRequired();

			builder.Property(x => x.TriggerName)
				.HasColumnName($"{prefix}trigger_name")
				.HasColumnType("text")
				.IsRequired();

			builder.Property(x => x.TriggerGroup)
				.HasColumnName($"{prefix}trigger_group")
				.HasColumnType("text")
				.IsRequired();

			builder.Property(x => x.InstanceName)
				.HasColumnName($"{prefix}instance_name")
				.HasColumnType("text")
				.IsRequired();

			builder.Property(x => x.FiredTime)
				.HasColumnName($"{prefix}fired_time")
				.HasColumnType("bigint")
				.IsRequired();

			builder.Property(x => x.ScheduledTime)
				.HasColumnName($"{prefix}sched_time")
				.HasColumnType("bigint")
				.IsRequired();

			builder.Property(x => x.Priority)
				.HasColumnName($"{prefix}priority")
				.HasColumnType("integer")
				.IsRequired();

			builder.Property(x => x.State)
				.HasColumnName($"{prefix}state")
				.HasColumnType("text")
				.IsRequired();

			builder.Property(x => x.JobName)
				.HasColumnName($"{prefix}job_name")
				.HasColumnType("text");

			builder.Property(x => x.JobGroup)
				.HasColumnName($"{prefix}job_group")
				.HasColumnType("text");

			builder.Property(x => x.IsNonConcurrent)
				.HasColumnName($"{prefix}is_nonconcurrent")
				.HasColumnType("bool")
				.IsRequired();

			builder.Property(x => x.RequestsRecovery)
				.HasColumnName($"{prefix}requests_recovery")
				.HasColumnType("bool");

			builder.HasIndex(x => x.TriggerName)
				.HasDatabaseName("idx_qrtz_ft_trig_name");

			builder.HasIndex(x => x.TriggerGroup)
				.HasDatabaseName("idx_qrtz_ft_trig_group");

			builder.HasIndex(x => new {x.SchedulerName, x.TriggerName, x.TriggerGroup})
				.HasDatabaseName("idx_qrtz_ft_trig_nm_gp");

			builder.HasIndex(x => x.InstanceName)
				.HasDatabaseName("idx_qrtz_ft_trig_inst_name");

			builder.HasIndex(x => x.JobName)
				.HasDatabaseName("idx_qrtz_ft_job_name");

			builder.HasIndex(x => x.JobGroup)
				.HasDatabaseName("idx_qrtz_ft_job_group");

			builder.HasIndex(x => x.RequestsRecovery)
				.HasDatabaseName("idx_qrtz_ft_job_req_recovery");
		}
	}
}