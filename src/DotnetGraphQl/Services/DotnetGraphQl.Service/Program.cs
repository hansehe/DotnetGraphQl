using DotnetGraphQl.Config;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace DotnetGraphQl.Service
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        private static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseConfiguration(Startup.Configuration)
                .UseKestrel((context, options) =>
                {
                    var port = context.Configuration.GetValue<int>("host:port");
                    options.ListenAnyIP(port);
                })
                .UseStartup<Startup>();
    }
}