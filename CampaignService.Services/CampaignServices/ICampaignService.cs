using CampaignService.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CampaignService.Services.CampaignServices
{
    public interface ICampaignService
    {
        Task<ICollection<CampaignModel>> GetActiveCampaignsWithCustomerMail(string email);
        Task<ICollection<CampaignModel>> GetAllActiveCampaigns();
    }
}