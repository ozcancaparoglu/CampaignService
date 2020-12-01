using CampaignService.Common.Cache;
using CampaignService.Data.Domains;
using CampaignService.Data.MapperConfiguration;
using CampaignService.Data.Models;
using CampaignService.Logging;
using CampaignService.Repositories;
using CampaignService.UnitOfWorks;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CampaignService.Services.CampaignFilterServices
{
    public class CampaignFilterService : ICampaignFilterService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IAutoMapperConfiguration autoMapper;
        private readonly IRedisCache redisCache;
        private readonly ILoggerManager loggerManager;

        private readonly IGenericRepository<CampaignService_CampaignFilter> campaignFilterRepo;

        public CampaignFilterService(IUnitOfWork unitOfWork, IAutoMapperConfiguration autoMapper, IRedisCache redisCache, ILoggerManager loggerManager)
        {
            this.unitOfWork = unitOfWork;
            this.autoMapper = autoMapper;
            this.redisCache = redisCache;
            this.loggerManager = loggerManager;

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



        #endregion
    }
}
