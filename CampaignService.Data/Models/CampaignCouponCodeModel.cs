using CampaignService.Common.Entities;
using System;

namespace CampaignService.Data.Models
{
    public class CampaignCouponCodeModel : EntityBaseModel
    {
        public int CampaignId { get; set; }
        public string CouponCode { get; set; }
        public int UsageNumber { get; set; }
        public int Used { get; set; }
        public bool? SingleUse { get; set; }
        public int? OrderId { get; set; }
        public int? CustomerId { get; set; }
        public bool CodeDelivery { get; set; }
        public string CustomFieldsXml { get; set; }
        public DateTime CreatedOnUtc { get; set; }

        public virtual CampaignModel Campaign { get; set; }
    }
}