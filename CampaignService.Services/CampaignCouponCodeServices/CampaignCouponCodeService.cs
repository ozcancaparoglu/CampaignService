using CampaignService.Common.Cache;
using CampaignService.Common.Services;
using CampaignService.Data.Domains;
using CampaignService.Data.MapperConfiguration;
using CampaignService.Data.Models;
using CampaignService.Logging;
using CampaignService.Repositories;
using CampaignService.UnitOfWorks;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CampaignService.Services.CampaignCouponCodeServices
{
    public class CampaignCouponCodeService : CommonService, ICampaignCouponCodeService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IAutoMapperConfiguration autoMapper;
        private readonly IRedisCache redisCache;
        private readonly ILoggerManager loggerManager;

        private readonly IGenericRepository<CampaignService_CampaignCouponCode> couponCodeRepo;

        public CampaignCouponCodeService(IUnitOfWork unitOfWork, IAutoMapperConfiguration autoMapper, IRedisCache redisCache, ILoggerManager loggerManager)
        {
            this.unitOfWork = unitOfWork;
            this.autoMapper = autoMapper;
            this.redisCache = redisCache;
            this.loggerManager = loggerManager;

            couponCodeRepo = this.unitOfWork.Repository<CampaignService_CampaignCouponCode>();
        }

        #region Db Methods

        public async Task<ICollection<CampaignCouponCodeModel>> GetCouponCodesWithCampaignId(int campaignId)
        {
            if (!redisCache.IsCached($"{CacheStatics.CouponCode}_{campaignId}"))
            {
                var entityList = await couponCodeRepo.FindAllAsync(x => x.CampaignId == campaignId);

                var modelList = autoMapper.MapCollection<CampaignService_CampaignCouponCode, CampaignCouponCodeModel>(entityList);

                await redisCache.SetAsync($"{CacheStatics.CouponCode}_{campaignId}", modelList, CacheStatics.CouponCodeCacheTime);
            }

            return await redisCache.GetAsync<ICollection<CampaignCouponCodeModel>>($"{CacheStatics.CouponCode}_{campaignId}");
        }

        #endregion

        #region Filter Methods

        #endregion
    }
}
