﻿using CampaignService.Common.Cache;
using CampaignService.Common.Services;
using CampaignService.Data.Domains;
using CampaignService.Data.MapperConfiguration;
using CampaignService.Data.Models;
using CampaignService.Repositories;
using CampaignService.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CampaignService.Services.CampaignServices
{
    public class CampaignService : CommonService, ICampaignService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IAutoMapperConfiguration autoMapper;
        private readonly IRedisCache redisCache;

        private readonly IGenericRepository<CampaignService_Campaigns> campaignRepo;

        public CampaignService(IUnitOfWork unitOfWork, IAutoMapperConfiguration autoMapper, IRedisCache redisCache)
        {
            this.unitOfWork = unitOfWork;
            this.autoMapper = autoMapper;
            this.redisCache = redisCache;

            campaignRepo = this.unitOfWork.Repository<CampaignService_Campaigns>();
        }

        #region Async(Db) Methods

        /// <summary>
        /// Gets all active and valid campaigns
        /// </summary>
        /// <returns></returns>
        public async Task<ICollection<CampaignModel>> GetAllActiveCampaigns()
        {
            if (!redisCache.IsCached(CacheStatics.AllActiveCampaigns))
            {
                DateTime now = DateTime.UtcNow;
                var entityList = await campaignRepo.FindAllAsync(x => x.IsActive == true && (x.StartDate <= now && x.EndDate >= now));
                await redisCache.SetAsync(CacheStatics.AllActiveCampaigns, entityList, CacheStatics.AllActiveCampaignsCacheTime);
            }

            return autoMapper.MapCollection<CampaignService_Campaigns, CampaignModel>
                (await redisCache.GetAsync<ICollection<CampaignService_Campaigns>>(CacheStatics.AllActiveCampaigns))
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
        public ICollection<CampaignModel> GetActiveCampaignsWithCustomerMail(string email, ICollection<CampaignModel> modelList)
        {
            return FilterPredication(modelList,
                x => !string.IsNullOrWhiteSpace(x.Customers) && x.Customers.Contains(email),
                x => string.IsNullOrWhiteSpace(x.Customers));
        }

        /// <summary>
        /// Active campaigns that can be benefited filter by domain email address
        /// </summary>
        /// <param name="email">Customer email domain</param>
        /// <param name="modelList">Active campaigns</param>
        /// <returns></returns>
        public ICollection<CampaignModel> GetActiveCampaignsWithCustomerMailDomain(string email, ICollection<CampaignModel> modelList)
        {
            string emailDomain = $"@{email.Split('@')[1]}";

            return FilterPredication(modelList,
                x => !string.IsNullOrWhiteSpace(x.CorporateDomainNames) && x.CorporateDomainNames.Contains(emailDomain),
                x => string.IsNullOrWhiteSpace(x.CorporateDomainNames));
        }

        /// <summary>
        /// Active campaigns that can be benefited filter by device type
        /// </summary>
        /// <param name="deviceType">Device type</param>
        /// <param name="modelList">Active campaigns</param>
        /// <returns></returns>
        public ICollection<CampaignModel> GetActiveCampaignsWithDeviceTypes(string deviceType, ICollection<CampaignModel> modelList)
        {
            return FilterPredication(modelList,
                x => !string.IsNullOrWhiteSpace(x.DeviceTypes) && x.DeviceTypes.Contains(deviceType),
                x => string.IsNullOrWhiteSpace(x.DeviceTypes));
        }

        /// <summary>
        /// Active campaigns that can be benefited filter by installment count
        /// </summary>
        /// <param name="installmentCount">Installment count</param>
        /// <param name="modelList">Active campaigns</param>
        /// <returns></returns>
        public ICollection<CampaignModel> GetActiveCampaignsWithInstallmentCount(int installmentCount, ICollection<CampaignModel> modelList)
        {
            return FilterPredication(modelList,
                x => x.InstallmentCount > 0 && x.InstallmentCount == installmentCount,
                x => x.InstallmentCount == 0);
        }

        /// <summary>
        /// Active campaigns that can be benefited filter by country
        /// </summary>
        /// <param name="countryId">Country id</param>
        /// <param name="modelList">Active campaigns</param>
        /// <returns></returns>
        public ICollection<CampaignModel> GetActiveCampaignsWithCountryId(string countryId, ICollection<CampaignModel> modelList)
        {
            return FilterPredication(modelList,
                x => !string.IsNullOrWhiteSpace(x.CountryIds) && x.CountryIds.Contains(countryId),
                x => string.IsNullOrWhiteSpace(x.CountryIds));
        }

        /// <summary>
        /// Active campaigns that can be benefited filter by pick-up (mağazadan teslimat)
        /// </summary>
        /// <param name="pickUp">Pickup</param>
        /// <param name="modelList">Active campaigns</param>
        /// <returns></returns>
        public ICollection<CampaignModel> GetActiveCampaignsWithPickUp(bool pickUp, ICollection<CampaignModel> modelList)
        {
            return FilterPredication(modelList,
                x => x.PickupInStore == pickUp,
                x => x.PickupInStore == false);
        }

        /// <summary>
        /// Active campaigns that can be benefited filter by bankname
        /// </summary>
        /// <param name="bankName">Bank name</param>
        /// <param name="modelList">Active campaigns</param>
        /// <returns></returns>
        public ICollection<CampaignModel> GetActiveCampaignsWithBankName(string bankName, ICollection<CampaignModel> modelList)
        {
            return FilterPredication(modelList,
                x => x.SelectedPaymentBankNames == bankName,
                x => string.IsNullOrWhiteSpace(x.SelectedPaymentBankNames));
        }

        /// <summary>
        /// Active campaigns that can be benefited filter by credit card bankname
        /// </summary>
        /// <param name="cartbankName">Credit card bankname</param>
        /// <param name="modelList">Active campaigns</param>
        /// <returns></returns>
        public ICollection<CampaignModel> GetActiveCampaignsWithCreditCartBankName(string cartbankName, ICollection<CampaignModel> modelList)
        {
            return FilterPredication(modelList,
                x => x.SelectedPaymentCreditCartBankNames == cartbankName,
                x => string.IsNullOrWhiteSpace(x.SelectedPaymentCreditCartBankNames));
        }

        /// <summary>
        /// Active campaigns that can be benefited filter by payment method
        /// </summary>
        /// <param name="paymentMethodSystemName">Payment method system name</param>
        /// <param name="modelList">Active campaigns</param>
        /// <returns></returns>
        public ICollection<CampaignModel> GetActiveCampaignsWithPaymentMethodSystemName(string paymentMethodSystemName, ICollection<CampaignModel> modelList)
        {
            return FilterPredication(modelList,
                x => x.PaymentMethodSystemNames == paymentMethodSystemName,
                x => string.IsNullOrWhiteSpace(x.PaymentMethodSystemNames));
        }


        #endregion
    }
}
