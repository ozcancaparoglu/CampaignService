using CampaignService.Common.Models;
using CampaignService.Data.Models;
using CampaignService.Services.CampaignFilterServices;
using CampaignService.Services.CampaignServices;
using CampaignService.Services.ShippingMethodServices;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using CampaignService.Services.CampaignUsageHistoryServices;
using CampaignService.Services.OrderServices;

namespace CampaignService.Services.FilterServices
{
    public class FilterService : IFilterService
    {
        #region Fields
        private readonly ICampaignService campaignService;
        private readonly IShippingMethodService shippingMethodService;
        private readonly ICampaignFilterService campaignFilterService;
        private readonly ICampaignUsageHistoryService campaignUsageHistoryService;
        private readonly IOrderService orderService;
        #endregion

        #region Ctor
        public FilterService(ICampaignService campaignService, 
            IShippingMethodService shippingMethodService, 
            ICampaignFilterService campaignFilterService,
            ICampaignUsageHistoryService campaignUsageHistoryService,
            IOrderService orderService)
        {
            this.campaignService = campaignService;
            this.shippingMethodService = shippingMethodService;
            this.campaignFilterService = campaignFilterService;
            this.campaignUsageHistoryService = campaignUsageHistoryService;
            this.orderService = orderService;
        }
        #endregion

        #region Methods

        public async Task<ICollection<CampaignModel>> FilteredCampaigns(CampaignRequest request)
        {
            var filteredCampaigns = await campaignService.GetAllActiveCampaigns();

            filteredCampaigns = campaignUsageHistoryService.FilterCampaignsWithUsageHistory(request.CustomerId, filteredCampaigns);
            filteredCampaigns = campaignFilterService.FilterCampaignsWithCampaignFilter(request.CustomerId, filteredCampaigns);
            filteredCampaigns = orderService.FilterRestrictedNthOrder(request.CustomerId, filteredCampaigns);

            filteredCampaigns = campaignService.FilterCampaignsWithCustomerRoleId(request.CustomerRoleIds.ToList(), filteredCampaigns);

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
