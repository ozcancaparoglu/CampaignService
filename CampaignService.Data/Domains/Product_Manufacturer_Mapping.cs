using CampaignService.Common.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CampaignService.Data.Domains
{
    public partial class Product_Manufacturer_Mapping : EntityBase
    {
        public int ProductId { get; set; }
        public int ManufacturerId { get; set; }
        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }
    }
}
