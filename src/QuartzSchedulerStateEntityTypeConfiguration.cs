using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AppAny.Quartz.EntityFrameworkCore.Migrations.PostgreSQL
{
	public class QuartzSchedulerStateEntityTypeConfiguration : IEntityTypeConfiguration<QuartzSchedulerState>
	{
		private readonly string prefix;
		private readonly string? schema;

		public QuartzSchedulerStateEntityTypeConfiguration(string prefix, string? schema)
		{
			this.prefix = prefix;
			this.schema = schema;
		}

		public void Configure(EntityTypeBuilder<QuartzSchedulerState> builder)
		{
			builder.ToTable("scheduler_state", schema);

			builder.HasKey(x => new {x.SchedulerName, x.InstanceName});

			builder.Property(x => x.SchedulerName)
				.HasColumnName($"{prefix}sched_name")
				.HasColumnType("text")
				.IsRequired();

			builder.Property(x => x.InstanceName)
				.HasColumnName($"{prefix}instance_name")
				.HasColumnType("text")
				.IsRequired();

			builder.Property(x => x.LastCheckInTime)
				.HasColumnName($"{prefix}last_checkin_time")
				.HasColumnType("bigint")
				.IsRequired();

			builder.Property(x => x.CheckInInterval)
				.HasColumnName($"{prefix}checkin_interval")
				.HasColumnType("bigint")
				.IsRequired();
		}
	}
}