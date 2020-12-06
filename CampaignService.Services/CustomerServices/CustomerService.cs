using CampaignService.Common.Cache;
using CampaignService.Common.Services;
using CampaignService.Data.Domains;
using CampaignService.Data.MapperConfiguration;
using CampaignService.Data.Models;
using CampaignService.Logging;
using CampaignService.Repositories;
using CampaignService.UnitOfWorks;
using System.Threading.Tasks;

namespace CampaignService.Services.CustomerServices
{
    public class CustomerService : CommonService, ICustomerService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IAutoMapperConfiguration autoMapper;
        private readonly IRedisCache redisCache;
        private readonly ILoggerManager loggerManager;

        private readonly IGenericRepository<Customer> customerRepo;

        public CustomerService(IUnitOfWork unitOfWork,
            IAutoMapperConfiguration autoMapper,
            IRedisCache redisCache,
            ILoggerManager loggerManager)
        {
            this.unitOfWork = unitOfWork;
            this.autoMapper = autoMapper;
            this.redisCache = redisCache;
            this.loggerManager = loggerManager;

            customerRepo = this.unitOfWork.Repository<Customer>();
        }

        #region Db Methods

        /// <summary>
        /// Gets customer by id
        /// </summary>
        /// <param name="id">Customer id</param>
        /// <returns>Customer Model</returns>
        public async Task<CustomerModel> GetCustomerById(int id)
        {
            if (!redisCache.IsCached($"{CacheStatics.Customer}_{id}"))
            {
                var entity = await customerRepo.GetByIdAsync(id);
                await redisCache.SetAsync($"{CacheStatics.Customer}_{id}", entity, CacheStatics.CampaignFiltersCacheTime);
            }

            return autoMapper.MapObject<Customer, CustomerModel>
                (await redisCache.GetAsync<Customer>($"{CacheStatics.Customer}_{id}"));
        }

        #endregion
    }
}
