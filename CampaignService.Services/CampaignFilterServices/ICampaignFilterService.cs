using CampaignService.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CampaignService.Services.CampaignFilterServices
{
    public interface ICampaignFilterService
    {
        
        #region Db Methods

        /// <summary>
        /// Gets campaign filters by campaignId arranges redis key and caches
        /// </summary>
        /// <param name="campaignId">Campaign id</param>
        /// <returns>CampaignFilterModel list</returns>
        Task<ICollection<CampaignFilterModel>> GetCampaignFilters(int campaignId);

        #endregion

        /// <summary>
        /// Active campaigns that can be benefited filter by campaign filters
        /// </summary>
        /// <param name="customerId">Customer id</param>
        /// <param name="modelList">Active campaigns</param>
        /// <returns></returns>
        ICollection<CampaignModel> FilterCampaignsWithCampaignFilter(int customerId, ICollection<CampaignModel> modelList);
    }
}