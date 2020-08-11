using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AppAny.Quartz.EntityFrameworkCore.Migrations.PostgreSQL
{
	public class QuartzCronTriggerEntityTypeConfiguration : IEntityTypeConfiguration<QuartzCronTrigger>
	{
		public void Configure(EntityTypeBuilder<QuartzCronTrigger> builder)
		{
			builder.ToTable("cron_triggers");

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

			builder.Property(x => x.CronExpression)
				.HasColumnName("cron_expression")
				.HasColumnType("text")
				.IsRequired();

			builder.Property(x => x.TimeZoneId)
				.HasColumnName("time_zone_id")
				.HasColumnType("text");

			builder.HasOne(x => x.Trigger)
				.WithMany(x => x.CronTriggers)
				.HasForeignKey(x => new {x.SchedulerName, x.TriggerName, x.TriggerGroup})
				.OnDelete(DeleteBehavior.Cascade);
		}
	}
}