using CampaignService.Common.Entities;
using System;

namespace CampaignService.Data.Domains
{
    public partial class CampaignService_CampaignCouponUsage : EntityBase
    {
        public int CustomerId { get; set; }
        public int CampaignId { get; set; }
        public bool Used { get; set; }
        public DateTime CreatedOnUtc { get; set; }

        public virtual CampaignService_Campaigns Campaign { get; set; }
    }
}
