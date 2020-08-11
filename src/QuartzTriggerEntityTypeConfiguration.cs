using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AppAny.Quartz.EntityFrameworkCore.Migrations.PostgreSQL
{
	public class QuartzTriggerEntityTypeConfiguration : IEntityTypeConfiguration<QuartzTrigger>
	{
		private readonly string prefix;
		private readonly string? schema;

		public QuartzTriggerEntityTypeConfiguration(string prefix, string? schema)
		{
			this.prefix = prefix;
			this.schema = schema;
		}

		public void Configure(EntityTypeBuilder<QuartzTrigger> builder)
		{
			builder.ToTable("triggers", schema);

			builder.HasKey(x => new {x.SchedulerName, x.TriggerName, x.TriggerGroup});

			builder.Property(x => x.SchedulerName)
				.HasColumnName($"{prefix}sched_name")
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

			builder.Property(x => x.JobName)
				.HasColumnName($"{prefix}job_name")
				.HasColumnType("text")
				.IsRequired();

			builder.Property(x => x.JobGroup)
				.HasColumnName($"{prefix}job_group")
				.HasColumnType("text")
				.IsRequired();

			builder.Property(x => x.Description)
				.HasColumnName($"{prefix}description")
				.HasColumnType("text");

			builder.Property(x => x.NextFireTime)
				.HasColumnName($"{prefix}next_fire_time")
				.HasColumnType("bigint");

			builder.Property(x => x.PreviousFireTime)
				.HasColumnName($"{prefix}prev_fire_time")
				.HasColumnType("bigint");

			builder.Property(x => x.Priority)
				.HasColumnName($"{prefix}priority")
				.HasColumnType("integer");

			builder.Property(x => x.TriggerState)
				.HasColumnName($"{prefix}trigger_state")
				.HasColumnType("text")
				.IsRequired();

			builder.Property(x => x.TriggerType)
				.HasColumnName($"{prefix}trigger_type")
				.HasColumnType("text")
				.IsRequired();

			builder.Property(x => x.StartTime)
				.HasColumnName($"{prefix}start_time")
				.HasColumnType("bigint")
				.IsRequired();

			builder.Property(x => x.EndTime)
				.HasColumnName($"{prefix}end_time")
				.HasColumnType("bigint");

			builder.Property(x => x.CalendarName)
				.HasColumnName($"{prefix}calendar_name")
				.HasColumnType("text");

			builder.Property(x => x.MisfireInstruction)
				.HasColumnName($"{prefix}misfire_instr")
				.HasColumnType("smallint");

			builder.Property(x => x.JobData)
				.HasColumnName($"{prefix}job_data")
				.HasColumnType("bytea");

			builder.HasOne(x => x.JobDetail)
				.WithMany(x => x.Triggers)
				.HasForeignKey(x => new {x.SchedulerName, x.JobName, x.JobGroup})
				.IsRequired();

			builder.HasIndex(x => x.NextFireTime)
				// TODO: Prefix?
				.HasDatabaseName("idx_qrtz_t_next_fire_time");
			
			builder.HasIndex(x => x.TriggerState)
				// TODO: Prefix?
				.HasDatabaseName("idx_qrtz_t_state");

			builder.HasIndex(x => new {x.NextFireTime, x.TriggerState})
				.HasDatabaseName("idx_qrtz_t_nft_st");
		}
	}
}