using Architecture.Core.DependencyService;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace Architecture.Presentation.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.Configure(
                            (o) => o.ApplicationServices = new DependencyInjector()
                        );
                    webBuilder.UseStartup<Startup>();
                });
    }
}
