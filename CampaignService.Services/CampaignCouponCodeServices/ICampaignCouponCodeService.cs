using CampaignService.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CampaignService.Services.CampaignCouponCodeServices
{
    internal interface ICampaignCouponCodeService
    {
        Task<ICollection<CampaignCouponCodeModel>> GetCouponCodesWithCampaignId(int campaignId);
    }
}