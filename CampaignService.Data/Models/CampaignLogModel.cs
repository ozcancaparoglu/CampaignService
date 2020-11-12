using CampaignService.Common.Entities;
using System;

namespace CampaignService.Data.Models
{
    public class CampaignLogModel : EntityBaseModel
    {
        public string CampaignRequest { get; set; }
        public string CampaignResponse { get; set; }
        public DateTime CreatedOnUtc { get; set; }
    }
}
