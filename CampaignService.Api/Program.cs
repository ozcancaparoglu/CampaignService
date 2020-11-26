using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Fluent;
using NLog.Web;
using System;

namespace CampaignService.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //var logger = NLog.Web.NLogBuilder.ConfigureNLog("Nlog.config").GetCurrentClassLogger();
            //try
            //{
                //logger.WithProperty("EntityType", "deneme")
                //    .Info("Farklı bir versiyon");
                //logger.Info("Deniyoruz");
                CreateWebHostBuilder(args).Build().Run();
            //}
            //catch (Exception exception)
            //{
            //    logger.Error(exception, "Stopped program because of exception");
            //    throw;
            //}
            //finally
            //{
            //    NLog.LogManager.Shutdown();
            //}
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) => 
            WebHost.CreateDefaultBuilder(args).ConfigureLogging((logging) =>
            {
                logging.ClearProviders();
                logging.SetMinimumLevel(LogLevel.None);
            }).UseNLog().UseStartup<Startup>();

    }
}
