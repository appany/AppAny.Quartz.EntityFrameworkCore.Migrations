using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AppAny.Quartz.EntityFrameworkCore.Migrations.PostgreSQL
{
	public class QuartzPausedTriggerGroupEntityTypeConfiguration : IEntityTypeConfiguration<QuartzPausedTriggerGroup>
	{
		public void Configure(EntityTypeBuilder<QuartzPausedTriggerGroup> builder)
		{
			builder.ToTable("paused_trigger_grps");

			builder.HasKey(x => new {x.SchedulerName, x.TriggerGroup});

			builder.Property(x => x.SchedulerName)
				.HasColumnName("sched_name")
				.HasColumnType("text")
				.IsRequired();

			builder.Property(x => x.TriggerGroup)
				.HasColumnName("trigger_group")
				.HasColumnType("text")
				.IsRequired();
		}
	}
}