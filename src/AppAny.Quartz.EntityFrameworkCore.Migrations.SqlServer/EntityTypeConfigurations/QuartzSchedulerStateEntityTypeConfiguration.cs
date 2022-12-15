namespace AppAny.Quartz.EntityFrameworkCore.Migrations.SqlServer
{
  using Microsoft.EntityFrameworkCore;
  using Microsoft.EntityFrameworkCore.Metadata.Builders;

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
      builder.ToTable($"{prefix}SCHEDULER_STATE", schema);

      builder.HasKey(x => new { x.SchedulerName, x.InstanceName });

      builder.Property(x => x.SchedulerName)
        .HasColumnName("SCHED_NAME")
        .HasMaxLength(120)
        .IsUnicode()
        .IsRequired();

      builder.Property(x => x.InstanceName)
        .HasColumnName("INSTANCE_NAME")
        .HasMaxLength(200)
        .IsUnicode()
        .IsRequired();

      builder.Property(x => x.LastCheckInTime)
        .HasColumnName("LAST_CHECKIN_TIME")
        .IsRequired();

      builder.Property(x => x.CheckInInterval)
        .HasColumnName("CHECKIN_INTERVAL")
        .IsRequired();
    }
  }
}
