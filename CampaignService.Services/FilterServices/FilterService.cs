using CampaignService.Common.Models;
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

            filteredCampaigns = campaignService.GetActiveCampaignsWithCustomerMail(request.Email, filteredCampaigns);

            filteredCampaigns = campaignService.GetActiveCampaignsWithCustomerMailDomain(request.Email, filteredCampaigns);

            filteredCampaigns = campaignService.GetActiveCampaignsWithDeviceTypes(request.DeviceType.ToString(), filteredCampaigns);

            filteredCampaigns = campaignService.GetActiveCampaignsWithInstallmentCount(request.InstallmentCount, filteredCampaigns);

            filteredCampaigns = campaignService.GetActiveCampaignsWithPickUp(request.PickupInStore, filteredCampaigns);

            filteredCampaigns = shippingMethodService.GetActiveCampaignsWithShippingMethod(request.LastShippingOption, filteredCampaigns);

            filteredCampaigns = campaignService.GetActiveCampaignsWithBankName(request.BankName, filteredCampaigns);

            filteredCampaigns = campaignService.GetActiveCampaignsWithCreditCartBankName(request.CardBankName, filteredCampaigns);

            filteredCampaigns = campaignService.GetActiveCampaignsWithPaymentMethodSystemName(request.PaymentMethodSystemName, filteredCampaigns);

            return filteredCampaigns;

        }

        #endregion


        #region Validations

        #endregion
    }
}
