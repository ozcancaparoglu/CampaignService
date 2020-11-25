using CampaignService.Data.Models;
using CampaignService.Service.Model;
using CampaignService.Services.CampaignServices;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CampaignService.Services.FilterServices
{
    public class FilterService : IFilterService
    {
        private readonly ICampaignService campaignService;

        public FilterService(ICampaignService campaignService)
        {
            this.campaignService = campaignService;
        }

        #region Methods

        public async Task<ICollection<CampaignModel>> FilteredCampaigns(CampaignRequest request)
        {
            var filteredCampaigns = await campaignService.GetAllActiveCampaigns();

            filteredCampaigns = campaignService.GetActiveCampaignsWithCustomerMail(request.Email, filteredCampaigns);
            filteredCampaigns = campaignService.GetActiveCampaignsWithCustomerMailDomain(request.Email, filteredCampaigns);
            filteredCampaigns = campaignService.GetActiveCampaignsWithDeviceTypes(request.DeviceType.ToString(), filteredCampaigns);
            filteredCampaigns = campaignService.GetActiveCampaignsWithInstallmentCount(request.InstallmentCount, filteredCampaigns);
            filteredCampaigns = campaignService.GetActiveCampaignsWithPickUp(request.PickupInStore, filteredCampaigns);

            //TODO: Shipping methods filter process must be done.

            return filteredCampaigns;
            
        }


        #endregion


        #region Validations

        #endregion
    }
}
