using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AppAny.Quartz.EntityFrameworkCore.Migrations.PostgreSQL
{
	public class QuartzSchedulerStateEntityTypeConfiguration : IEntityTypeConfiguration<QuartzSchedulerState>
	{
		private readonly string? prefix;
		private readonly string? schema;

		public QuartzSchedulerStateEntityTypeConfiguration(string? prefix, string? schema)
		{
			this.prefix = prefix;
			this.schema = schema;
		}

		public void Configure(EntityTypeBuilder<QuartzSchedulerState> builder)
		{
			builder.ToTable($"{prefix}scheduler_state", schema);

			builder.HasKey(x => new {x.SchedulerName, x.InstanceName});

			builder.Property(x => x.SchedulerName)
				.HasColumnName("sched_name")
				.HasColumnType("text")
				.IsRequired();

			builder.Property(x => x.InstanceName)
				.HasColumnName("instance_name")
				.HasColumnType("text")
				.IsRequired();

			builder.Property(x => x.LastCheckInTime)
				.HasColumnName("last_checkin_time")
				.HasColumnType("bigint")
				.IsRequired();

			builder.Property(x => x.CheckInInterval)
				.HasColumnName("checkin_interval")
				.HasColumnType("bigint")
				.IsRequired();
		}
	}
}