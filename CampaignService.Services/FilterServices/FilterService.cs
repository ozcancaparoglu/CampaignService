﻿using CampaignService.Common.Models;
using CampaignService.Data.Models;
using CampaignService.Services.CampaignServices;
using CampaignService.Services.ShippingMethodServices;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CampaignService.Services.FilterServices
{
    public class FilterService : IFilterService
    {
        #region Fields
        private readonly ICampaignService campaignService;
        private readonly IShippingMethodService shippingMethodService;
        #endregion

        #region Ctor
        public FilterService(ICampaignService campaignService, IShippingMethodService shippingMethodService)
        {
            this.campaignService = campaignService;
            this.shippingMethodService = shippingMethodService;
        }
        #endregion

        #region Methods

        public async Task<ICollection<CampaignModel>> FilteredCampaigns(CampaignRequest request)
        {
            var filteredCampaigns = await campaignService.GetAllActiveCampaigns();

            filteredCampaigns = campaignService.FilterCampaignsWithCustomerMail(request.Email, filteredCampaigns);
            filteredCampaigns = campaignService.FilterCampaignsWithCustomerMailDomain(request.Email, filteredCampaigns);
            filteredCampaigns = campaignService.FilterCampaignsWithDeviceTypes(request.DeviceType.ToString(), filteredCampaigns);
            filteredCampaigns = campaignService.FilterCampaignsWithInstallmentCount(request.InstallmentCount, filteredCampaigns);
            filteredCampaigns = campaignService.FilterCampaignsWithPickUp(request.PickupInStore, filteredCampaigns);
            
            filteredCampaigns = campaignService.FilterCampaignsWithBankName(request.BankName, filteredCampaigns);
            filteredCampaigns = campaignService.FilterCampaignsWithCreditCartBankName(request.CardBankName, filteredCampaigns);
            filteredCampaigns = campaignService.FilterCampaignsWithPaymentMethodSystemName(request.PaymentMethodSystemName, filteredCampaigns);

            filteredCampaigns = shippingMethodService.FilterCampaignsWithShippingMethod(request.LastShippingOption, filteredCampaigns);

            return filteredCampaigns;

        }

        #endregion


        #region Validations

        #endregion
    }
}
