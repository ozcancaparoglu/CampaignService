using CampaignService.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CampaignService.Services.CampaignServices
{
    public interface ICampaignService
    {
        Task<ICollection<CampaignModel>> GetAllActiveCampaigns();
        ICollection<CampaignModel> GetActiveCampaignsWithCountryId(string countryId, ICollection<CampaignModel> modelList);
        ICollection<CampaignModel> GetActiveCampaignsWithCustomerMail(string email, ICollection<CampaignModel> modelList);
        ICollection<CampaignModel> GetActiveCampaignsWithCustomerMailDomain(string email, ICollection<CampaignModel> modelList);
        ICollection<CampaignModel> GetActiveCampaignsWithDeviceTypes(string deviceType, ICollection<CampaignModel> modelList);
        ICollection<CampaignModel> GetActiveCampaignsWithInstallmentCount(int installmentCount, ICollection<CampaignModel> modelList);
        ICollection<CampaignModel> GetActiveCampaignsWithPickUp(bool pickUp, ICollection<CampaignModel> modelList);
        
    }
}