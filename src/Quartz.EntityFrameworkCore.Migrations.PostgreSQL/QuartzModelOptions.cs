namespace Quartz.EntityFrameworkCore.Migrations.PostgreSQL
{
	public class QuartzModelOptions
	{
		public string? Prefix { get; set; } = "qrtz_";
		public string? Schema { get; set; }
	}
}