using Autofac;
using CampaignService.Services.CampaignServices;
using CampaignService.Services.Ioc;

namespace CampaignService.UnitTests
{
    public class TestBase
    {
        private Autofac.IContainer _autoFacContainer;

        protected Autofac.IContainer AutoFacContainer
        {
            get
            {
                if (_autoFacContainer == null) 
                {
                    var builder = new ContainerBuilder();
                    builder.RegisterModule(new IocModule());
                    var container = builder.Build();
                    _autoFacContainer = container;
                }

                return _autoFacContainer;
            }
        }

        protected ICampaignService CampaignService
        {
            get
            {
                return AutoFacContainer.Resolve<ICampaignService>();
            }
        }
    }
}
