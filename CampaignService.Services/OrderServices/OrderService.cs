using CampaignService.Common.Cache;
using CampaignService.Common.Services;
using CampaignService.Data.Domains;
using CampaignService.Data.MapperConfiguration;
using CampaignService.Logging;
using CampaignService.Repositories;
using CampaignService.UnitOfWorks;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CampaignService.Services.OrderServices
{
    public class OrderService : CommonService, IOrderService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IAutoMapperConfiguration autoMapper;
        private readonly IRedisCache redisCache;
        private readonly ILoggerManager loggerManager;

        private IGenericRepository<Order> orderRepo;

        public OrderService(IUnitOfWork unitOfWork, IAutoMapperConfiguration autoMapper, IRedisCache redisCache, ILoggerManager loggerManager)
        {
            this.unitOfWork = unitOfWork;
            this.autoMapper = autoMapper;
            this.redisCache = redisCache;
            this.loggerManager = loggerManager;

            orderRepo = this.unitOfWork.Repository<Order>();
        }

        #region Db Methods

        public async Task<decimal> GetCustomerOrdersTotalInGivenTime(int customerId, DateTime startDate, DateTime endDate)
        {
            var entityList = await orderRepo.FindAllAsync(x => x.CustomerId == customerId && x.CreatedOnUtc >= startDate && x.CreatedOnUtc <= endDate);
            
            return entityList.Sum(x => x.OrderTotal);
        }

        #endregion
    }
}
