using DbServer.Wallet.Application;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace DbServer.Wallet.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseContentRoot(AppSettings.BasePath)
                .UseUrls(AppSettings.BindingUrl)
                .UseStartup<Startup>();
    }
}