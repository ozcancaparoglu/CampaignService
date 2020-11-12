using CampaignService.Common.Entities;
using System;

namespace CampaignService.Data.Models
{
    public class CampaignCouponUsageModel : EntityBaseModel
    {

        public int CustomerId { get; set; }
        public int CampaignId { get; set; }
        public bool Used { get; set; }
        public DateTime CreatedOnUtc { get; set; }

        public virtual CampaignModel Campaign { get; set; }
    }
}