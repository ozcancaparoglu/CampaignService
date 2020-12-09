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
    public class CampaignCouponCodeUsageService : CommonService, ICampaignCouponCodeUsageService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IAutoMapperConfiguration autoMapper;
        private readonly IRedisCache redisCache;
        private readonly ILoggerManager loggerManager;

        private readonly IGenericRepository<CampaignService_CampaignCouponUsage> couponUsageRepo;

        public CampaignCouponCodeUsageService(IUnitOfWork unitOfWork, IAutoMapperConfiguration autoMapper, IRedisCache redisCache, ILoggerManager loggerManager)
        {
            this.unitOfWork = unitOfWork;
            this.autoMapper = autoMapper;
            this.redisCache = redisCache;
            this.loggerManager = loggerManager;

            couponUsageRepo = this.unitOfWork.Repository<CampaignService_CampaignCouponUsage>();
        }

        #region Db Methods

        public async Task<ICollection<CampaignCouponUsageModel>> GetCouponUsageByCustomer(int customerId)
        {
            var entityList = await couponUsageRepo.FindAllAsync(x => x.CustomerId == customerId);

            return autoMapper.MapCollection<CampaignService_CampaignCouponUsage, CampaignCouponUsageModel>(entityList).ToList();
        }

        #endregion

        #region Filter Methods

        public ICollection<CampaignModel> FilterCampaignCouponUsage(int customerId, ICollection<CampaignModel> modelList) 
        {
            var couponCodeSaveList = modelList.Where(x => x.CouponSave).ToList();

            if (couponCodeSaveList == null || couponCodeSaveList.Count == 0)
                return modelList;

            var exceptCampaignIds = new List<int>();
            var customerCouponUsageList = GetCouponUsageByCustomer(customerId).Result;

            foreach (var campaign in couponCodeSaveList)
            {
                var check = customerCouponUsageList.FirstOrDefault(x => x.CampaignId == campaign.Id && !x.Used);

                if (check != null)
                    exceptCampaignIds.Add(campaign.Id);
            }

            modelList = modelList.Where(x => !exceptCampaignIds.Contains(x.Id)).ToList();

            return modelList;
        }

        #endregion
    }
}
