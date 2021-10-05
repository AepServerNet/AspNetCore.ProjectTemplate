using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace Template_SQLite_AdoNet_Crud
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateWebHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webHostBuilder => {
                    webHostBuilder.UseStartup<Startup>();
                })
                ;
    }
}
