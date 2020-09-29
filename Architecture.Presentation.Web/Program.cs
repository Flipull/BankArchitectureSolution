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
                            //what can we do with IApplicationBuilder.Use(delegate middleware)
                            (o) => o.ApplicationServices = new DependencyInjector()
                            //this does not work, 100% even tho it seems like a better solution
                            //then copying the old DepInjector into a new one, in the Startup class
                        );
                    webBuilder.UseStartup<Startup>();
                });
    }
}
