using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Sunny.Consul.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Sunny.Consul.Configuration
{
    public class Program
    {
        //public static IConfiguration Configutatin { get; } = new ConfigurationBuilder()
        //    .SetBasePath(Directory.GetCurrentDirectory())
        //    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true).Build();

        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                }).ConfigureAppConfiguration(builder => {
                    builder.AddConsul("Sunny/appsettings.json", options => {  // ÅäÖÃÖÐÐÄ
                        options.ConsulConfigurationOptions = cco => { cco.Address = new Uri("http://localhost:8500"); /* new Uri(Configutatin.GetSection("ConsulOptions:Address").Value);*/ };
                        options.Optional = true;
                        options.PollWaitTime = TimeSpan.FromSeconds(5);
                        options.ReloadOnChange = true;
                    }).AddEnvironmentVariables();
                });
    }
}
