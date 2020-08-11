using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AppAny.Quartz.EntityFrameworkCore.Migrations.PostgreSQL
{
	public class QuartzPausedTriggerGroupEntityTypeConfiguration : IEntityTypeConfiguration<QuartzPausedTriggerGroup>
	{
		private readonly string prefix;
		private readonly string? schema;

		public QuartzPausedTriggerGroupEntityTypeConfiguration(string prefix, string? schema)
		{
			this.prefix = prefix;
			this.schema = schema;
		}

		public void Configure(EntityTypeBuilder<QuartzPausedTriggerGroup> builder)
		{
			builder.ToTable("paused_trigger_grps", schema);

			builder.HasKey(x => new {x.SchedulerName, x.TriggerGroup});

			builder.Property(x => x.SchedulerName)
				.HasColumnName($"{prefix}sched_name")
				.HasColumnType("text")
				.IsRequired();

			builder.Property(x => x.TriggerGroup)
				.HasColumnName($"{prefix}trigger_group")
				.HasColumnType("text")
				.IsRequired();
		}
	}
}