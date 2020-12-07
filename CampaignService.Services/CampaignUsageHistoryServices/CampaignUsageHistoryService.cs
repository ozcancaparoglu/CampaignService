using CampaignService.Common.Cache;
using CampaignService.Common.Enums;
using CampaignService.Common.Services;
using CampaignService.Data.Domains;
using CampaignService.Data.MapperConfiguration;
using CampaignService.Data.Models;
using CampaignService.Logging;
using CampaignService.Repositories;
using CampaignService.Services.OrderServices;
using CampaignService.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CampaignService.Services.CampaignUsageHistoryServices
{
    public class CampaignUsageHistoryService : CommonService, ICampaignUsageHistoryService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IAutoMapperConfiguration autoMapper;
        private readonly IRedisCache redisCache;
        private readonly ILoggerManager loggerManager;

        private readonly IOrderService orderService;

        private readonly IGenericRepository<CampaignService_CampaignUsageHistory> campaignUsageHistoryRepo;

        public CampaignUsageHistoryService(IUnitOfWork unitOfWork, IAutoMapperConfiguration autoMapper, IRedisCache redisCache, ILoggerManager loggerManager,
            IOrderService orderService)
        {
            this.unitOfWork = unitOfWork;
            this.autoMapper = autoMapper;
            this.redisCache = redisCache;
            this.loggerManager = loggerManager;

            this.orderService = orderService;

            campaignUsageHistoryRepo = this.unitOfWork.Repository<CampaignService_CampaignUsageHistory>();
        }

        #region Db Methods

        /// <summary>
        /// Gets CampaignUsageHistory by customer id and campaign id
        /// </summary>
        /// <param name="customerId">Customer id</param>
        /// <param name="campaignId">Campaign id</param>
        /// <returns>CampaignUsageHistoryModel list</returns>
        public ICollection<CampaignUsageHistoryModel> GetCampaignUsageHistories(int customerId, int campaignId)
        {
            var entityList = campaignUsageHistoryRepo.Filter(x => x.CustomerId == customerId && x.CampaignId == campaignId, null, "Campaign");

            return autoMapper.MapCollection<CampaignService_CampaignUsageHistory, CampaignUsageHistoryModel>(entityList).ToList();
        }

        #endregion

        #region Filter Methods

        public ICollection<CampaignModel> FilterCampaignsWithUsageHistory(int customerId, ICollection<CampaignModel> modelList)
        {
            var campaignsUsageHistory = new List<CampaignUsageHistoryModel>();

            foreach (CampaignModel campaign in modelList)
                campaignsUsageHistory.AddRange(GetCampaignUsageHistories(customerId, campaign.Id));

            if (campaignsUsageHistory == null || campaignsUsageHistory.Count == 0)
                return modelList;

            var exceptCampaignIds = FilterCampaignLimitations(customerId, campaignsUsageHistory);

            if (exceptCampaignIds == null || exceptCampaignIds.Count == 0)
                return modelList;

            modelList = modelList.Where(x => !exceptCampaignIds.Contains(x.Id)).ToList();

            return modelList;
        }

        private ICollection<int> FilterCampaignLimitations(int customerId, List<CampaignUsageHistoryModel> campaignUsageHistoryModels)
        {
            //TODO: Refactor algoritmayı zorlamışlar böyle db tutulmaz.
            var exceptCampaignIdList = new List<int>();
            var groupedCampaignUsageList = campaignUsageHistoryModels.GroupBy(g => g.CampaignId).Select(grp => grp.ToList()).ToList();

            foreach (var groupedCampaignUsage in groupedCampaignUsageList)
            {
                var campaignModel = groupedCampaignUsage.FirstOrDefault();
                switch (campaignModel.Campaign.CampaignUsageLimitationType)
                {
                    case (int)CampaignLimitationType.Unlimited:
                        continue;

                    case (int)CampaignLimitationType.NTimesOnly:
                    case (int)CampaignLimitationType.NTimesPerCustomer:
                        if (groupedCampaignUsage.Count > campaignModel.Campaign.CampaignUsageLimitationCount)
                            exceptCampaignIdList.Add(campaignModel.Id);
                        break;

                    case (int)CampaignLimitationType.NTimesPerCustomerPerCalendarYear:
                        if (orderService.GetCustomerOrdersTotalInGivenTime(customerId, DateTime.Now, DateTime.Now.AddYears(-1)).Result > campaignModel.Campaign.BuyConditionCustomerPreviousOrdersTotal)
                            exceptCampaignIdList.Add(campaignModel.Id);
                        break;

                    case (int)CampaignLimitationType.NTimesPerCustomerPerDay:
                        if (orderService.GetCustomerOrdersTotalInGivenTime(customerId, DateTime.Now, DateTime.Now.AddDays(-1)).Result > campaignModel.Campaign.BuyConditionCustomerPreviousOrdersTotal)
                            exceptCampaignIdList.Add(campaignModel.Id);
                        break;
                    default:
                        break;
                }
            }

            return exceptCampaignIdList;
        }

        #endregion
    }
}
