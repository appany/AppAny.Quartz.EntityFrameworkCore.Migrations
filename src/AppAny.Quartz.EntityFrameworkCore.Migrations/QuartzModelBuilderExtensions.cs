namespace AppAny.Quartz.EntityFrameworkCore.Migrations
{
  public static class QuartzModelBuilderExtensions
  {
    public static QuartzModelBuilder UseNoPrefix(this QuartzModelBuilder builder)
    {
      return builder.UsePrefix(null);
    }
  }
}
