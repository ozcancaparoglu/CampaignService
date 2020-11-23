﻿using CampaignService.Common.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CampaignService.Data.Models
{
    public partial class ProductManufacturerMappingModel : EntityBaseModel
    {
        public int ProductId { get; set; }
        public int ManufacturerId { get; set; }
        public bool IsFeaturedProduct { get; set; }
        public int DisplayOrder { get; set; }
    }
}
