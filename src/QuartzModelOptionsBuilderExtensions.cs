namespace AppAny.Quartz.EntityFrameworkCore.Migrations.PostgreSQL
{
	public static class QuartzModelOptionsBuilderExtensions
	{
		public static IQuartzModelOptionsBuilder UsePrefix(this IQuartzModelOptionsBuilder builder, string? prefix)
		{
			builder.Options.Prefix = prefix;

			return builder;
		}
		
		public static IQuartzModelOptionsBuilder UseNoPrefix(this IQuartzModelOptionsBuilder builder)
		{
			builder.Options.Prefix = null;

			return builder;
		}

		public static IQuartzModelOptionsBuilder UseSchema(this IQuartzModelOptionsBuilder builder, string schema)
		{
			builder.Options.Schema = schema;

			return builder;
		}
	}
}