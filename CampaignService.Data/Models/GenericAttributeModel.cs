using CampaignService.Common.Entities;

namespace CampaignService.Data.Models
{
    public class GenericAttributeModel : EntityBaseModel
    {
        public int EntityId { get; set; }
        public string KeyGroup { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
        public int StoreId { get; set; }
    }
}
