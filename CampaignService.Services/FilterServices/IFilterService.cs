using CampaignService.Data.Models;
using CampaignService.Service.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CampaignService.Services.FilterServices
{
    public interface IFilterService
    {
        Task<ICollection<CampaignModel>> FilteredCampaigns(CampaignRequest request);
    }
}