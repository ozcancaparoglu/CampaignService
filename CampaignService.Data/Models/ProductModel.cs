﻿using CampaignService.Common.Entities;
using System;

namespace CampaignService.Data.Models
{
    public class ProductModel : EntityBaseModel
    {
        public int ProductTypeId { get; set; }
        public int ParentGroupedProductId { get; set; }
        public bool VisibleIndividually { get; set; }
        public string Name { get; set; }
        public string NameAlternative { get; set; }
        public string ShortDescription { get; set; }
        public string FullDescription { get; set; }
        public string DetailedDescription { get; set; }
        public string AdminComment { get; set; }
        public int ProductTemplateId { get; set; }
        public int VendorId { get; set; }
        public bool ShowOnHomePage { get; set; }
        public string MetaKeywords { get; set; }
        public string MetaDescription { get; set; }
        public string MetaTitle { get; set; }
        public bool AllowCustomerReviews { get; set; }
        public int ApprovedRatingSum { get; set; }
        public int NotApprovedRatingSum { get; set; }
        public int ApprovedTotalReviews { get; set; }
        public int NotApprovedTotalReviews { get; set; }
        public bool SubjectToAcl { get; set; }
        public bool LimitedToStores { get; set; }
        public string Sku { get; set; }
        public string ManufacturerPartNumber { get; set; }
        public string Gtin { get; set; }
        public bool IsGiftCard { get; set; }
        public int GiftCardTypeId { get; set; }
        public decimal? OverriddenGiftCardAmount { get; set; }
        public bool RequireOtherProducts { get; set; }
        public string RequiredProductIds { get; set; }
        public bool AutomaticallyAddRequiredProducts { get; set; }
        public bool IsDownload { get; set; }
        public int DownloadId { get; set; }
        public bool UnlimitedDownloads { get; set; }
        public int MaxNumberOfDownloads { get; set; }
        public int? DownloadExpirationDays { get; set; }
        public int DownloadActivationTypeId { get; set; }
        public bool HasSampleDownload { get; set; }
        public int SampleDownloadId { get; set; }
        public bool HasUserAgreement { get; set; }
        public string UserAgreementText { get; set; }
        public bool IsRecurring { get; set; }
        public int RecurringCycleLength { get; set; }
        public int RecurringCyclePeriodId { get; set; }
        public int RecurringTotalCycles { get; set; }
        public bool IsRental { get; set; }
        public int RentalPriceLength { get; set; }
        public int RentalPricePeriodId { get; set; }
        public bool IsShipEnabled { get; set; }
        public bool IsFreeShipping { get; set; }
        public bool ShipSeparately { get; set; }
        public decimal AdditionalShippingCharge { get; set; }
        public int DeliveryDateId { get; set; }
        public bool IsTaxExempt { get; set; }
        public int TaxCategoryId { get; set; }
        public bool IsTelecommunicationsOrBroadcastingOrElectronicServices { get; set; }
        public int ManageInventoryMethodId { get; set; }
        public bool UseMultipleWarehouses { get; set; }
        public int WarehouseId { get; set; }
        public int StockQuantity { get; set; }
        public bool DisplayStockAvailability { get; set; }
        public bool DisplayStockQuantity { get; set; }
        public int MinStockQuantity { get; set; }
        public int LowStockActivityId { get; set; }
        public int NotifyAdminForQuantityBelow { get; set; }
        public int BackorderModeId { get; set; }
        public bool AllowBackInStockSubscriptions { get; set; }
        public int OrderMinimumQuantity { get; set; }
        public int OrderMaximumQuantity { get; set; }
        public string AllowedQuantities { get; set; }
        public bool AllowAddingOnlyExistingAttributeCombinations { get; set; }
        public bool DisableBuyButton { get; set; }
        public bool DisableWishlistButton { get; set; }
        public bool AvailableForPreOrder { get; set; }
        public DateTime? PreOrderAvailabilityStartDateTimeUtc { get; set; }
        public bool CallForPrice { get; set; }
        public decimal Price { get; set; }
        public decimal OldPrice { get; set; }
        public decimal ProductCost { get; set; }
        public decimal? SpecialOldPrice { get; set; }
        public decimal? SpecialPrice { get; set; }
        public DateTime? SpecialPriceStartDateTimeUtc { get; set; }
        public DateTime? SpecialPriceEndDateTimeUtc { get; set; }
        public bool CustomerEntersPrice { get; set; }
        public decimal MinimumCustomerEnteredPrice { get; set; }
        public decimal MaximumCustomerEnteredPrice { get; set; }
        public bool BasepriceEnabled { get; set; }
        public decimal BasepriceAmount { get; set; }
        public int BasepriceUnitId { get; set; }
        public decimal BasepriceBaseAmount { get; set; }
        public int BasepriceBaseUnitId { get; set; }
        public bool MarkAsNew { get; set; }
        public DateTime? MarkAsNewStartDateTimeUtc { get; set; }
        public DateTime? MarkAsNewEndDateTimeUtc { get; set; }
        public bool HasTierPrices { get; set; }
        public bool HasDiscountsApplied { get; set; }
        public decimal Weight { get; set; }
        public decimal Length { get; set; }
        public decimal Width { get; set; }
        public decimal Height { get; set; }
        public DateTime? AvailableStartDateTimeUtc { get; set; }
        public DateTime? AvailableEndDateTimeUtc { get; set; }
        public int DisplayOrder { get; set; }
        public bool Published { get; set; }
        public bool Deleted { get; set; }
        public DateTime CreatedOnUtc { get; set; }
        public DateTime UpdatedOnUtc { get; set; }
        public string ErpCode { get; set; }
        public string ErpName { get; set; }
        public string ErpTypeCode { get; set; }
        public string ErpTypeName { get; set; }
        public string ErpBarcode { get; set; }
        public string ErpStatus { get; set; }
        public string MeasureValue { get; set; }
        public string MeasureUnit { get; set; }
        public bool DisableErpSync { get; set; }
        public string VideoUrl1 { get; set; }
        public string VideoUrl2 { get; set; }
        public long ViewCount { get; set; }
        public long SaleCount { get; set; }
        public DateTime? ErpInsertDate { get; set; }
        public string BillingName { get; set; }
        public string ModelNumber { get; set; }
        public int MinimumQuantityForFreeShipping { get; set; }
        public bool? AllowInstallments { get; set; }
        public bool AllowPickupStore { get; set; }
        public bool HideOnSearch { get; set; }
        public bool HiddenFromCorporateCustomer { get; set; }
        public bool PreventErpExportInOrder { get; set; }
        public bool IsFlexMediaActive { get; set; }
        public bool? HasMultiplePrice { get; set; }
        public int OldProductId { get; set; }
        public string ColorCode { get; set; }
        public string CssColorCode { get; set; }
        public bool? IsPackage { get; set; }
        public bool ShowDiscountBadge { get; set; }
        public string ProductNote { get; set; }
        public bool OnlyMarketPlaceSale { get; set; }
        public string MarketPlaceSku { get; set; }
        public string MarketplaceName { get; set; }
    }
}
