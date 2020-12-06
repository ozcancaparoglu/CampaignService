using CampaignService.Common.Cache;
using CampaignService.Common.Enums;
using CampaignService.Common.Models;
using CampaignService.Common.Services;
using CampaignService.Data.Domains;
using CampaignService.Data.MapperConfiguration;
using CampaignService.Data.Models;
using CampaignService.Logging;
using CampaignService.Repositories;
using CampaignService.Services.CustomerServices;
using CampaignService.Services.GenericAttributeServices;
using CampaignService.UnitOfWorks;
using Newtonsoft.Json;
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
        private readonly ICustomerService customerService;

        private readonly IGenericRepository<CampaignService_CampaignFilters> campaignFilterRepo;

        public CampaignFilterService(IUnitOfWork unitOfWork, 
            IAutoMapperConfiguration autoMapper, 
            IRedisCache redisCache, 
            ILoggerManager loggerManager,
            IGenericAttributeService genericAttributeService,
            ICustomerService customerService)
        {
            this.unitOfWork = unitOfWork;
            this.autoMapper = autoMapper;
            this.redisCache = redisCache;
            this.loggerManager = loggerManager;

            this.genericAttributeService = genericAttributeService;
            this.customerService = customerService;

            campaignFilterRepo = this.unitOfWork.Repository<CampaignService_CampaignFilters>();
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

            return autoMapper.MapCollection<CampaignService_CampaignFilters, CampaignFilterModel>
                (await redisCache.GetAsync<ICollection<CampaignService_CampaignFilters>>($"{CacheStatics.CampaignFilters}_{campaignId}"))
                .ToList();
        }

        #endregion

        #region Filter Methods

        /// <summary>
        /// Active campaigns that can be benefited filter by campaign filters
        /// </summary>
        /// <param name="customerId">Customer id</param>
        /// <param name="modelList">Active campaigns</param>
        /// <returns></returns>
        public ICollection<CampaignModel> FilterCampaignsWithCampaignFilter(int customerId, ICollection<CampaignModel> modelList)
        {
            var campaignsFilters = new List<CampaignFilterModel>();

            foreach (CampaignModel campaign in modelList)
            {
                campaignsFilters.AddRange(GetCampaignFilters(campaign.Id).Result);
            }

            if (campaignsFilters == null || campaignsFilters.Count == 0)
                return modelList;

            var exceptCampaignIds = new List<int>();
            exceptCampaignIds.AddRange(FilterAccountAndSegment(customerId, campaignsFilters));
            exceptCampaignIds.AddRange(FilterLoyaltyCardNumber(customerId, campaignsFilters));

            if (exceptCampaignIds == null || exceptCampaignIds.Count == 0)
                return modelList;

            modelList = modelList.Where(x => !exceptCampaignIds.Contains(x.Id)).ToList();

            return modelList;
        }

        /// <summary>
        /// Filtering campaign by account and segment type
        /// </summary>
        /// <param name="customerId">Customer id</param>
        /// <param name="campaignFilterModelList">CampaignFilter list</param>
        /// <returns>Campaign id's which are going to exclude from active campaigns list</returns>
        private ICollection<int> FilterAccountAndSegment(int customerId, List<CampaignFilterModel> campaignFilterModelList)
        {
            var exceptCampaignIdList = new List<int>();
            var accountAndSegmentFilters = campaignFilterModelList.Where(x => x.FilterType == CampaignFilters.Account || x.FilterType == CampaignFilters.Segment).ToList();

            if (accountAndSegmentFilters == null || accountAndSegmentFilters.Count == 0)
                return exceptCampaignIdList;

            AccountAndSegment accountAndSegment = null;

            var customerAccountAndSegment = genericAttributeService.GetGenericAttribute(new GenericAttributeModel
            {
                EntityId = customerId,
                KeyGroup = GenericAttributeKeyAndGroups.Customer,
                Key = GenericAttributeKeyAndGroups.AccountAndSegment
            }).Result;

            if (customerAccountAndSegment == null && string.IsNullOrWhiteSpace(customerAccountAndSegment.Value))
                return exceptCampaignIdList;

            accountAndSegment = JsonConvert.DeserializeObject<AccountAndSegment>(customerAccountAndSegment.Value);
            bool checkFilterValue = false;

            foreach (var campaignFilter in accountAndSegmentFilters)
            {
                //TODO: Db böyle yazacağınız value batsın.
                var filterValue = campaignFilter.FilterValue.Split("=")[1]; 

                if (campaignFilter.FilterType == CampaignFilters.Segment)
                {
                    checkFilterValue = accountAndSegment.CustomerSegments.Any(x => x.Key == filterValue || x.Value == filterValue);
                    if (!checkFilterValue)
                        exceptCampaignIdList.Add(campaignFilter.CampaignId);
                }
                //TODO: Account örneği bulamadım db'lerde aynıdır segment'le diye tahmin ediyorum. Tekrar kontrol edilecek
                else if (campaignFilter.FilterType == CampaignFilters.Account)
                {
                    checkFilterValue = accountAndSegment.AccountIds.Any(x => x.Key == filterValue || x.Value == filterValue);
                    if (!checkFilterValue)
                        exceptCampaignIdList.Add(campaignFilter.CampaignId);
                }
            }

            return exceptCampaignIdList;
        }

        /// <summary>
        /// Filtering campaign by loyalty card existance
        /// </summary>
        /// <param name="customerId">Customer id</param>
        /// <param name="campaignFilterModelList">CampaignFilter list</param>
        /// <returns>Campaign id's which are going to exclude from active campaigns list</returns>
        private ICollection<int> FilterLoyaltyCardNumber(int customerId, List<CampaignFilterModel> campaignFilterModelList)
        {
            var exceptCampaignIdList = new List<int>();
            var loyaltyCardFilter = campaignFilterModelList.FirstOrDefault(x => x.FilterType == CampaignFilters.LoyaltyCard);

            if (loyaltyCardFilter == null)
                return exceptCampaignIdList;

            var customer = customerService.GetCustomerById(customerId).Result;

            //TODO: LoyaltyCardIsActive diye bir alan var veritabanında muhtemelen kullanılmıyor. Emin olun, kullanıyorsa is active kontrolü de yapılmalı.
            if (customer == null || string.IsNullOrWhiteSpace(customer.LoyaltyCardNumber))
                exceptCampaignIdList.Add(loyaltyCardFilter.CampaignId);

            return exceptCampaignIdList;
        }



        #endregion
    }
}
