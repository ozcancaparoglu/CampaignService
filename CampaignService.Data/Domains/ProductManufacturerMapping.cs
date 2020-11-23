using CampaignService.Common.Entities;
using System;
using System.Collections.Generic;

namespace CampaignService.Data.Domains
{
    public partial class ProductManufacturerMapping : EntityBase
    {
        public int ProductId { get; set; }
        public int ManufacturerId { get; set; }
        public bool IsFeaturedProduct { get; set; }
        public int DisplayOrder { get; set; }
    }
}
