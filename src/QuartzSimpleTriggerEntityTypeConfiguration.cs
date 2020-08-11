using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AppAny.Quartz.EntityFrameworkCore.Migrations.PostgreSQL
{
	public class QuartzSimpleTriggerEntityTypeConfiguration : IEntityTypeConfiguration<QuartzSimpleTrigger>
	{
		private readonly string prefix;
		private readonly string? schema;

		public QuartzSimpleTriggerEntityTypeConfiguration(string prefix, string? schema)
		{
			this.prefix = prefix;
			this.schema = schema;
		}

		public void Configure(EntityTypeBuilder<QuartzSimpleTrigger> builder)
		{
			builder.ToTable("simple_triggers", schema);

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

			builder.Property(x => x.RepeatCount)
				.HasColumnName($"{prefix}repeat_count")
				.HasColumnType("bigint")
				.IsRequired();

			builder.Property(x => x.RepeatInterval)
				.HasColumnName($"{prefix}repeat_interval")
				.HasColumnType("bigint")
				.IsRequired();

			builder.Property(x => x.TimesTriggered)
				.HasColumnName($"{prefix}times_triggered")
				.HasColumnType("bigint")
				.IsRequired();

			builder.HasOne(x => x.Trigger)
				.WithMany(x => x.SimpleTriggers)
				.HasForeignKey(x => new {x.SchedulerName, x.TriggerName, x.TriggerGroup})
				.OnDelete(DeleteBehavior.Cascade);
		}
	}
}