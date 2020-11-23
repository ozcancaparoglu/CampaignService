using Autofac;
using AutoMapper;
using CampaignService.Common.Cache;
using CampaignService.Data.Domains.Common;
using CampaignService.Data.MapperConfiguration;
using CampaignService.Logging;
using CampaignService.Repositories;
using CampaignService.UnitOfWorks;
using Microsoft.AspNetCore.Http;

namespace CampaignService.Services.Ioc
{
    public class IocModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<AppDbContext>(); //For Unit Tests.
            builder.RegisterType<CacheManager>().As<ICacheManager>().InstancePerLifetimeScope();
            builder.RegisterType<RedisCache>().As<IRedisCache>().InstancePerLifetimeScope();

            builder.RegisterType(typeof(UnitOfWork)).As(typeof(IUnitOfWork)).AsSelf().InstancePerLifetimeScope();
            builder.RegisterGeneric(typeof(GenericRepository<>)).As(typeof(IGenericRepository<>)).AsSelf().InstancePerLifetimeScope();

            builder.RegisterType(typeof(Mapper)).As(typeof(IMapper)).AsSelf().InstancePerLifetimeScope();
            builder.RegisterType(typeof(HttpContextAccessor)).As(typeof(IHttpContextAccessor)).AsSelf().InstancePerLifetimeScope();

            builder.RegisterType<AutoMapperConfiguration>().As<IAutoMapperConfiguration>();

            builder.RegisterType<LoggerManager>().As<ILoggerManager>();

            builder.RegisterAssemblyTypes(System.Reflection.Assembly.Load("CampaignService.Services"))
               .Where(t => t.Name.EndsWith("Service"))
               .AsImplementedInterfaces()
               .InstancePerLifetimeScope();

            base.Load(builder);
        }
        
    }
}
