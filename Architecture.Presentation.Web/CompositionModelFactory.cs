using Architecture.Core.CompositionService;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace Architecture.Presentation.Web
{
    //Needed when EF migrations runs on another library;
    //Which I stupidly did.
    //Move migrations back and remove this ugly thing.
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
