namespace AppAny.Quartz.EntityFrameworkCore.Migrations.Tests
{
  public static class TestSetup
  {
    public const string PostgreSqlConnectionString = "host=localhost; port=5432; db=test; uid=test; pwd=test;";
    public const string MySqlConnectionString = "server=localhost; port=33306; database=test; uid=test; pwd=test;";
    public const string SqlServerConnectionString = "Server=tcp:localhost,1433;Initial Catalog=test;Persist Security Info=True;User ID=sa;Password=Test12@.;Encrypt=True;TrustServerCertificate=True;";
  }
}
