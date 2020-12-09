using CampaignService.Data.Models;
using System.Collections.Generic;

namespace CampaignService.Services.CampaignCouponCodeServices
{
    public interface ICampaignCouponCodeUsageService
    {
        ICollection<CampaignModel> FilterCampaignCouponUsage(int customerId, ICollection<CampaignModel> modelList);
    }
}