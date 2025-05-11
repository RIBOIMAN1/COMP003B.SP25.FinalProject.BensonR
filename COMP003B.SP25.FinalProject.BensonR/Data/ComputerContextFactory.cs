using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
namespace COMP003B.SP25.FinalProject.BensonR.Data
{
	public class ComputerContextFactory : IDesignTimeDbContextFactory<ComputerContext>
	{
		public ComputerContext CreateDbContext(string[] args)
		{
			IConfigurationRoot configuration = new ConfigurationBuilder()
			.SetBasePath(Directory.GetCurrentDirectory())
			.AddJsonFile("appsettings.json")
			.Build();
			var connectionString = configuration.GetConnectionString("DefaultConnection");
			var optionsBuilder = new DbContextOptionsBuilder<ComputerContext>();
			optionsBuilder.UseSqlServer(connectionString);
			return new ComputerContext(optionsBuilder.Options);
		}
	}
}