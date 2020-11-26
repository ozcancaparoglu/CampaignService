using Autofac;
using Autofac.Extensions.DependencyInjection;
using CampaignService.Data.Domains.Common;
using CampaignService.Logging;
using CampaignService.Logging.CampaignService.Logging;
using CampaignService.Services.Ioc;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace CampaignService.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            //services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("ServerConnectionString")));

            services.AddDistributedRedisCache(option => {
                option.Configuration = Configuration["RedisConfiguration:Host"];
                option.InstanceName = Configuration["RedisConfiguration:RedisDB"];
            });

            services.AddControllers();
            services.AddRazorPages();

            var builder = new ContainerBuilder();
            builder.Populate(services);
            builder.RegisterModule(new IocModule());
            builder.RegisterType<LoggerManager>().As<ILoggerManager>().InstancePerLifetimeScope();
            var container = builder.Build();

            return new AutofacServiceProvider(container);

           

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseRouting();

            app.UseEndpoints(endpoints => { endpoints.MapRazorPages(); endpoints.MapControllers(); });

            app.UseHttpsRedirection();
        }
    }
}
