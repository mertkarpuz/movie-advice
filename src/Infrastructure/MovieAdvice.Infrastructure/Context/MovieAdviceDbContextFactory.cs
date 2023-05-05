using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.Reflection;


namespace MovieAdvice.Infrastructure.Context
{
    public class MovieAdviceDbContextFactory : IDesignTimeDbContextFactory<MovieAdviceDbContext>
    {
        public IConfiguration configuration;
        public MovieAdviceDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<MovieAdviceDbContext>();
            var folderPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(Path.Combine(folderPath, "sharedsettings.json"), false, true)
                .AddEnvironmentVariables()
                .Build();
            //AppUtilities.AppUtilitiesConfigure(configuration);
            optionsBuilder.UseSqlServer(GetDbConnectionString());
            return new MovieAdviceDbContext(optionsBuilder.Options);
        }

        private string GetDbConnectionString()
        {
            return configuration.GetSection("ConnectionStrings:SQLConnection").Value;
        }
    }
}
