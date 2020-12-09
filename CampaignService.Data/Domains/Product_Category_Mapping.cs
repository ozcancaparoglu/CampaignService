using CampaignService.Common.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CampaignService.Data.Domains
{
    public partial class Product_Category_Mapping : EntityBase
    {

        public int ProductId { get; set; }
        public int CategoryId { get; set; }
        [ForeignKey("ProductId")]
        public Product  Product{ get; set; }
    }
}
