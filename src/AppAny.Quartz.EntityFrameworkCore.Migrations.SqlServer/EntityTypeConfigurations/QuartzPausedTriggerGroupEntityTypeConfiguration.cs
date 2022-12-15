namespace AppAny.Quartz.EntityFrameworkCore.Migrations.SqlServer
{
  using Microsoft.EntityFrameworkCore;
  using Microsoft.EntityFrameworkCore.Metadata.Builders;

  public class QuartzPausedTriggerGroupEntityTypeConfiguration : IEntityTypeConfiguration<QuartzPausedTriggerGroup>
  {
    private readonly string? prefix;
    private readonly string? schema;

    public QuartzPausedTriggerGroupEntityTypeConfiguration(string? prefix, string? schema)
    {
      this.prefix = prefix;
      this.schema = schema;
    }

    public void Configure(EntityTypeBuilder<QuartzPausedTriggerGroup> builder)
    {
      builder.ToTable($"{prefix}PAUSED_TRIGGER_GRPS", schema);

      builder.HasKey(x => new { x.SchedulerName, x.TriggerGroup });

      builder.Property(x => x.SchedulerName)
        .HasColumnName("SCHED_NAME")
        .HasMaxLength(120)
        .IsUnicode()
        .IsRequired();

      builder.Property(x => x.TriggerGroup)
        .HasColumnName("TRIGGER_GROUP")
        .HasMaxLength(150)
        .IsUnicode()
        .IsRequired();
    }
  }
}
