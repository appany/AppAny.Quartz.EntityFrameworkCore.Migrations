using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AppAny.Quartz.EntityFrameworkCore.Migrations.PostgreSQL
{
  public class QuartzBlobTriggerEntityTypeConfiguration : IEntityTypeConfiguration<QuartzBlobTrigger>
  {
    private readonly string? prefix;
    private readonly string? schema;

    public QuartzBlobTriggerEntityTypeConfiguration(string? prefix, string? schema)
    {
      this.prefix = prefix;
      this.schema = schema;
    }

    public void Configure(EntityTypeBuilder<QuartzBlobTrigger> builder)
    {
      builder.ToTable($"{prefix}blob_triggers", schema);

      builder.HasKey(x => new { x.SchedulerName, x.TriggerName, x.TriggerGroup });

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

      builder.Property(x => x.BlobData)
        .HasColumnName("blob_data")
        .HasColumnType("bytea");

      builder.HasOne(x => x.Trigger)
        .WithMany(x => x.BlobTriggers)
        .HasForeignKey(x => new { x.SchedulerName, x.TriggerName, x.TriggerGroup })
        .OnDelete(DeleteBehavior.Cascade);
    }
  }
}
