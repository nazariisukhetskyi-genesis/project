using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Project_CSharp.Services;

namespace Project_CSharp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            FileService.ReadUsersFromFile();

            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
