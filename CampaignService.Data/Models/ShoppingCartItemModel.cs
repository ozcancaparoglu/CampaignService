using CampaignService.Common.Entities;
using CampaignService.Data.Domains;
using System;
using System.Collections.Generic;
using System.Text;

namespace CampaignService.Data.Models
{
    public partial class ShoppingCartItemModel : EntityBaseModel
    {
        public int StoreId { get; set; }
        public int ShoppingCartTypeId { get; set; }
        public int CustomerId { get; set; }
        public int ProductId { get; set; }
        public string AttributesXml { get; set; }
        public decimal CustomerEnteredPrice { get; set; }
        public int Quantity { get; set; }
        public DateTime? RentalStartDateUtc { get; set; }
        public DateTime? RentalEndDateUtc { get; set; }
        public DateTime CreatedOnUtc { get; set; }
        public DateTime UpdatedOnUtc { get; set; }
        public int PromotionTypeId { get; set; }
        public int? ParentId { get; set; }
        public int? ShoppingCartDealId { get; set; }
        public string MultiplePriceCode { get; set; }
        public byte? SystemPaymentType { get; set; }
        public byte InsertTypeId { get; set; }
        public string ProductNote { get; set; }
        public ICollection<int> ProductCategoryIds { get; set; }
        public string ProductSku { get; set; }
        public virtual ProductModel Product { get; set; }
    }
}
