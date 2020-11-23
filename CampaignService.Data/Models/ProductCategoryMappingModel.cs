using CampaignService.Common.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CampaignService.Data.Models
{
    public partial class ProductCategoryMappingModel : EntityBaseModel
    {
        public int ProductId { get; set; }
        public int CategoryId { get; set; }
        public bool IsFeaturedProduct { get; set; }
        public int DisplayOrder { get; set; }
        public bool? IsDefault { get; set; }
    }
}
