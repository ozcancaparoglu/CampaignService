using CampaignService.Common.Entities;
using System;


namespace CampaignService.Data.Domains
{
    public partial class CampaignService_CampaignUsageHistory : EntityBase
    {
        public int CampaignUsageType { get; set; }
        public int? CampaignTypeId { get; set; }
        public int CampaignId { get; set; }
        public string CampaignName { get; set; }
        public string CampaignDescription { get; set; }
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public string ErpCode { get; set; }
        public decimal DiscountAmount { get; set; }
        public string AffectedProductIds { get; set; }
        public string ProductIds { get; set; }
        public string CampaignCouponCode { get; set; }
        public DateTime? CreatedOnUtc { get; set; }

        public virtual CampaignService_Campaigns Campaign { get; set; }
    }
}
