﻿using CampaignService.Data.Models;
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
    }
}