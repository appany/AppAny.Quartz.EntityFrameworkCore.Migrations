using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AppAny.Quartz.EntityFrameworkCore.Migrations.MySql
{
  public class QuartzBlobTriggerEntityTypeConfiguration : IEntityTypeConfiguration<QuartzBlobTrigger>
  {
    private readonly string? prefix;

    public QuartzBlobTriggerEntityTypeConfiguration(string? prefix)
    {
      this.prefix = prefix;
    }

    public void Configure(EntityTypeBuilder<QuartzBlobTrigger> builder)
    {
      builder.ToTable($"{prefix}blob_triggers");

      builder.HasKey(x => new { x.SchedulerName, x.TriggerName, x.TriggerGroup });

      builder.Property(x => x.SchedulerName)
        .HasColumnName("sched_name")
        .HasColumnType("varchar(120)")
        .IsRequired();

      builder.Property(x => x.TriggerName)
        .HasColumnName("trigger_name")
        .HasColumnType("varchar(200)")
        .IsRequired();

      builder.Property(x => x.TriggerGroup)
        .HasColumnName("trigger_group")
        .HasColumnType("varchar(200)")
        .IsRequired();

      builder.Property(x => x.BlobData)
        .HasColumnName("blob_data")
        .HasColumnType("blob");

      builder.HasOne(x => x.Trigger)
        .WithMany(x => x.BlobTriggers)
        .HasForeignKey(x => new { x.SchedulerName, x.TriggerName, x.TriggerGroup })
        .OnDelete(DeleteBehavior.Cascade);
    }
  }
}
