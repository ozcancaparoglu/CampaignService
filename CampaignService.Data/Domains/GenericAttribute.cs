using CampaignService.Common.Entities;

namespace CampaignService.Data.Domains
{
    public partial class GenericAttribute: EntityBase
    {
        public int EntityId { get; set; }
        public string KeyGroup { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
        public int StoreId { get; set; }
    }
}
