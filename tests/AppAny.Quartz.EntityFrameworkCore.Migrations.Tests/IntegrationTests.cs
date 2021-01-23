using Xunit;
using Microsoft.EntityFrameworkCore;

namespace AppAny.Quartz.EntityFrameworkCore.Migrations.Tests
{
	public class IntegrationTests
	{
		[Fact]
		public void CompleteMigration()
		{
			var options = new DbContextOptionsBuilder<IntegrationDbContext>().UseNpgsql(TestSetup.ConnectionString).Options;

			using (var dbContext = new IntegrationDbContext(options))
			{
				dbContext.Database.Migrate();
			}
		}
	}
}
