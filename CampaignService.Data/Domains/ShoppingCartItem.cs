using CampaignService.Common.Entities;
using System;
using System.Collections.Generic;

namespace CampaignService.Data.Domains
{
    public partial class ShoppingCartItem : EntityBase
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
    }
}
