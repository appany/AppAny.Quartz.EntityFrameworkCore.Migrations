namespace AppAny.Quartz.EntityFrameworkCore.Migrations
{
  public static class QuartzModelBuilderExtensions
  {
    public static QuartzModelBuilder UseNoPrefix(this QuartzModelBuilder builder)
    {
      return builder.UsePrefix(null);
    }

    public static QuartzModelBuilder UseNoSchema(this QuartzModelBuilder builder)
    {
      return builder.UseSchema(null);
    }
  }
}
