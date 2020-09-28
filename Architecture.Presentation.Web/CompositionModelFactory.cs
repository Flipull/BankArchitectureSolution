using Architecture.Core.CompositionService;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace Architecture.Presentation.Web
{
    public class CompositionModelFactory : IDesignTimeDbContextFactory<CompositionModel>
    {
        public CompositionModel CreateDbContext(string[] args)
        {
            IConfiguration config =
                new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json")
                    .Build();

            return new CompositionModel(
                    new DbContextOptionsBuilder<CompositionModel>()
                        .UseSqlServer(config.GetConnectionString("BankDatabase"))
                        .Options
                );
        }
    }
}
