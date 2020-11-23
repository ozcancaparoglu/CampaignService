using CampaignService.Common.Entities;
using System;
using System.Collections.Generic;

namespace CampaignService.Data.Domains
{
    public partial class ProductCategoryMapping : EntityBase
    {
        public int ProductId { get; set; }
        public int CategoryId { get; set; }
        public bool IsFeaturedProduct { get; set; }
        public int DisplayOrder { get; set; }
        public bool? IsDefault { get; set; }
    }
}
