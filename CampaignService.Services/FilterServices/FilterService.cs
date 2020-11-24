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

            filteredCampaigns = await campaignService.GetActiveCampaignsWithCustomerMail(request.Email);

            return filteredCampaigns;
            
        }


        #endregion


        #region Validations

        #endregion
    }
}
