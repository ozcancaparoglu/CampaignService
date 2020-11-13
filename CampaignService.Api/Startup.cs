using Autofac;
using Autofac.Extensions.DependencyInjection;
using CampaignService.Data.Domains.Common;
using Microsoft.AspNetCore.Builder;
using CampaignService.Services.Ioc;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;


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
            services.AddMvc(opt =>
            {
                opt.EnableEndpointRouting = false;
            });

            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("ServerConnectionString")));

            var builder = new ContainerBuilder();
            builder.Populate(services);
            builder.RegisterModule(new IocModule());
            var container = builder.Build();

            return new AutofacServiceProvider(container);

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
            app.UseHttpsRedirection();
        }
    }
}
