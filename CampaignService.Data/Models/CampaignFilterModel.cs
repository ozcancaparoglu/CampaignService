using CampaignService.Common.Entities;
using System;

namespace CampaignService.Data.Models
{
    public class CampaignFilterModel : EntityBaseModel
    {
        public int CampaignId { get; set; }
        public string FilterType { get; set; }
        public string FilterValue { get; set; }
        public bool IsActive { get; set; }
        public string ErpCode { get; set; }
        public DateTime? CreatedOnUtc { get; set; }
    }
}
