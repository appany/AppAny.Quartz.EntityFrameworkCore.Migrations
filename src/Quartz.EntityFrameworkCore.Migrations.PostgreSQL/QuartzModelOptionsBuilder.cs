namespace Quartz.EntityFrameworkCore.Migrations.PostgreSQL
{
	public class QuartzModelOptionsBuilder : IQuartzModelOptionsBuilder
	{
		public QuartzModelOptionsBuilder(QuartzModelOptions options)
		{
			Options = options;
		}

		public QuartzModelOptions Options { get; }
	}
}