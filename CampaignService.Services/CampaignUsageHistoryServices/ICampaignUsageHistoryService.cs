using CampaignService.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CampaignService.Services.CampaignUsageHistoryServices
{
    public interface ICampaignUsageHistoryService
    {
        #region Db Methods

        /// <summary>
        /// Gets CampaignUsageHistory by customer id and campaign id
        /// </summary>
        /// <param name="customerId">Customer id</param>
        /// <param name="campaignId">Campaign id</param>
        /// <returns>CampaignUsageHistoryModel list</returns>
        ICollection<CampaignUsageHistoryModel> GetCampaignUsageHistories(int customerId, int campaignId);

        #endregion

        #region Filter Methods

        ICollection<CampaignModel> FilterCampaignsWithUsageHistory(int customerId, ICollection<CampaignModel> modelList);

        #endregion
    }
}