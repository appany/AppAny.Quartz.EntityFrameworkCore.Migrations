using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AppAny.Quartz.EntityFrameworkCore.Migrations.PostgreSQL
{
	public class QuartzSimplePropertyTriggerEntityTypeConfiguration
		: IEntityTypeConfiguration<QuartzSimplePropertyTrigger>
	{
		private readonly string prefix;
		private readonly string? schema;

		public QuartzSimplePropertyTriggerEntityTypeConfiguration(string prefix, string? schema)
		{
			this.prefix = prefix;
			this.schema = schema;
		}

		public void Configure(EntityTypeBuilder<QuartzSimplePropertyTrigger> builder)
		{
			builder.ToTable("SIMPROP_TRIGGERS", schema);

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

			builder.Property(x => x.StringProperty1)
				.HasColumnName($"{prefix}str_prop_1")
				.HasColumnType("text");

			builder.Property(x => x.StringProperty2)
				.HasColumnName($"{prefix}str_prop_2")
				.HasColumnType("text");

			builder.Property(x => x.StringProperty3)
				.HasColumnName($"{prefix}str_prop_3")
				.HasColumnType("text");

			builder.Property(x => x.IntegerProperty1)
				.HasColumnName($"{prefix}int_prop_1")
				.HasColumnType("integer");

			builder.Property(x => x.IntegerProperty2)
				.HasColumnName($"{prefix}int_prop_2")
				.HasColumnType("integer");

			builder.Property(x => x.LongProperty1)
				.HasColumnName($"{prefix}long_prop_1")
				.HasColumnType("bigint");

			builder.Property(x => x.LongProperty2)
				.HasColumnName($"{prefix}long_prop_2")
				.HasColumnType("bigint");

			builder.Property(x => x.DecimalProperty1)
				.HasColumnName($"{prefix}dec_prop_1")
				.HasColumnType("numeric");

			builder.Property(x => x.DecimalProperty2)
				.HasColumnName($"{prefix}dec_prop_2")
				.HasColumnType("numeric");

			builder.Property(x => x.BooleanProperty1)
				.HasColumnName($"{prefix}bool_prop_1")
				.HasColumnType("bool");

			builder.Property(x => x.BooleanProperty2)
				.HasColumnName($"{prefix}bool_prop_2")
				.HasColumnType("bool");

			builder.Property(x => x.TimeZoneId)
				.HasColumnName($"{prefix}time_zone_id")
				.HasColumnType("text");

			builder.HasOne(x => x.Trigger)
				.WithMany(x => x.SimplePropertyTriggers)
				.HasForeignKey(x => new {x.SchedulerName, x.TriggerName, x.TriggerGroup})
				.OnDelete(DeleteBehavior.Cascade);
		}
	}
}