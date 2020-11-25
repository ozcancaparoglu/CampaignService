using CampaignService.Common.Entities;

namespace CampaignService.Data.Models
{
    public class ShippingMethodModel : EntityBaseModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int DisplayOrder { get; set; }
        public bool? Published { get; set; }
        public bool? IsDirectlyIntegrated { get; set; }
        public string AllowedDays { get; set; }
        public string AllowedHours { get; set; }
        public string AllowedCountryIds { get; set; }
        public string AllowedStateProvinceIds { get; set; }
        public string AllowedCountyIds { get; set; }
        public string AllowedCategoryIds { get; set; }
        public string TrackingUrlFormat { get; set; }
        public decimal Rate { get; set; }
        public bool? AllowFreeShipping { get; set; }
    }
}
