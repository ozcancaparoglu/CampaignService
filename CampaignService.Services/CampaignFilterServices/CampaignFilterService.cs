using CampaignService.Common.Cache;
using CampaignService.Common.Enums;
using CampaignService.Common.Services;
using CampaignService.Data.Domains;
using CampaignService.Data.MapperConfiguration;
using CampaignService.Data.Models;
using CampaignService.Logging;
using CampaignService.Repositories;
using CampaignService.Services.GenericAttributeServices;
using CampaignService.UnitOfWorks;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CampaignService.Services.CampaignFilterServices
{
    public class CampaignFilterService : CommonService, ICampaignFilterService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IAutoMapperConfiguration autoMapper;
        private readonly IRedisCache redisCache;
        private readonly ILoggerManager loggerManager;

        private readonly IGenericAttributeService genericAttributeService;

        private readonly IGenericRepository<CampaignService_CampaignFilter> campaignFilterRepo;

        public CampaignFilterService(IUnitOfWork unitOfWork, IAutoMapperConfiguration autoMapper, IRedisCache redisCache, ILoggerManager loggerManager,
            IGenericAttributeService genericAttributeService)
        {
            this.unitOfWork = unitOfWork;
            this.autoMapper = autoMapper;
            this.redisCache = redisCache;
            this.loggerManager = loggerManager;

            this.genericAttributeService = genericAttributeService;

            campaignFilterRepo = this.unitOfWork.Repository<CampaignService_CampaignFilter>();
        }

        #region Db Methods

        /// <summary>
        /// Gets campaign filters by campaignId arranges redis key and caches
        /// </summary>
        /// <param name="campaignId">Campaign id</param>
        /// <returns>CampaignFilterModel list</returns>
        public async Task<ICollection<CampaignFilterModel>> GetCampaignFilters(int campaignId)
        {
            if (!redisCache.IsCached($"{CacheStatics.CampaignFilters}_{campaignId}"))
            {
                var entityList = await campaignFilterRepo.FindAllAsync(x => x.IsActive == true && x.CampaignId == campaignId);
                await redisCache.SetAsync($"{CacheStatics.CampaignFilters}_{campaignId}", entityList, CacheStatics.CampaignFiltersCacheTime);
            }

            return autoMapper.MapCollection<CampaignService_CampaignFilter, CampaignFilterModel>
                (await redisCache.GetAsync<ICollection<CampaignService_CampaignFilter>>($"{CacheStatics.CampaignFilters}_{campaignId}"))
                .ToList();
        }
        #endregion

        #region Filter Methods

        /// <summary>
        /// Active campaigns that can be benefited filter by customer email address
        /// </summary>
        /// <param name="email">Customer email</param>
        /// <param name="modelList">Active campaigns</param>
        /// <returns></returns>
        public ICollection<CampaignModel> FilterCampaignsWithCampaignFilter(int customerId, ICollection<CampaignModel> modelList)
        {
            //TODO: Refactor and UseCampaignFilter

            var customerAttributeAccountAndSegment = genericAttributeService.GetGenericAttribute(new GenericAttributeModel
            {
                EntityId = customerId,
                KeyGroup = GenericAttributeKeyAndGroups.Customer,
                Key = GenericAttributeKeyAndGroups.AccountAndSegment
            }).Result;

            LogRequestModel logRequestModel = new LogRequestModel()
            {
                EntityType = "GetActiveCampaignsWithCustomerMail"
            };
            loggerManager.LogInfo(logRequestModel);
            return FilterPredication(modelList,
                x => !string.IsNullOrWhiteSpace(x.Customers) && x.Customers.Contains("ozcan.caparoglu"),
                x => string.IsNullOrWhiteSpace(x.Customers));
        }



        #endregion
    }
}
