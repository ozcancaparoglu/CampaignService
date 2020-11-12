using CampaignService.Common.Entities;
using System;


namespace CampaignService.Data.Domains
{
    public partial class CampaignService_Log : EntityBase
    {
        public string CampaignRequest { get; set; }
        public string CampaignResponse { get; set; }
        public DateTime CreatedOnUtc { get; set; }
    }
}
