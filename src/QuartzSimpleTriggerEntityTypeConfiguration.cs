using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AppAny.Quartz.EntityFrameworkCore.Migrations.PostgreSQL
{
	public class QuartzSimpleTriggerEntityTypeConfiguration : IEntityTypeConfiguration<QuartzSimpleTrigger>
	{
		public void Configure(EntityTypeBuilder<QuartzSimpleTrigger> builder)
		{
			builder.ToTable("simple_triggers");

			builder.HasKey(x => new {x.SchedulerName, x.TriggerName, x.TriggerGroup});

			builder.Property(x => x.SchedulerName)
				.HasColumnName("sched_name")
				.HasColumnType("text")
				.IsRequired();

			builder.Property(x => x.TriggerName)
				.HasColumnName("trigger_name")
				.HasColumnType("text")
				.IsRequired();

			builder.Property(x => x.TriggerGroup)
				.HasColumnName("trigger_group")
				.HasColumnType("text")
				.IsRequired();

			builder.Property(x => x.RepeatCount)
				.HasColumnName("repeat_count")
				.HasColumnType("bigint")
				.IsRequired();

			builder.Property(x => x.RepeatInterval)
				.HasColumnName("repeat_interval")
				.HasColumnType("bigint")
				.IsRequired();

			builder.Property(x => x.TimesTriggered)
				.HasColumnName("times_triggered")
				.HasColumnType("bigint")
				.IsRequired();

			builder.HasOne(x => x.Trigger)
				.WithMany(x => x.SimpleTriggers)
				.HasForeignKey(x => new {x.SchedulerName, x.TriggerName, x.TriggerGroup})
				.OnDelete(DeleteBehavior.Cascade);
		}
	}
}