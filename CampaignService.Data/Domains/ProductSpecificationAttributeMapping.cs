using CampaignService.Common.Entities;
using System;
using System.Collections.Generic;

namespace CampaignService.Data.Domains
{
    public partial class ProductSpecificationAttributeMapping : EntityBase
    {
        public int ProductId { get; set; }
        public int AttributeTypeId { get; set; }
        public int SpecificationAttributeOptionId { get; set; }
        public string CustomValue { get; set; }
        public bool AllowFiltering { get; set; }
        public bool ShowOnProductPage { get; set; }
        public int DisplayOrder { get; set; }
    }
}
