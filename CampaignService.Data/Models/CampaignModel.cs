using CampaignService.Common.Entities;
using System;
using System.Collections.Generic;

namespace CampaignService.Data.Models
{
    public class CampaignModel : EntityBaseModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string ErpCode { get; set; }
        public bool IsActive { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime? CreatedOnUtc { get; set; }
        public bool? MergeWithOtherCampaigns { get; set; }
        public string CustomFieldsXml { get; set; }
        public string BuyConditionCategories { get; set; }
        public string BuyConditionIncludedSpecifications { get; set; }
        public string BuyConditionExcludedSpecifications { get; set; }
        public string BuyConditionExcludedProductIds { get; set; }
        public decimal BuyConditionCartTotal { get; set; }
        public decimal BuyConditionCustomerPreviousOrdersTotal { get; set; }
        public string FreeConditionCategories { get; set; }
        public string FreeConditionIncludedSpecifications { get; set; }
        public string FreeConditionExcludedSpecifications { get; set; }
        public string FreeConditionExcludedProductIds { get; set; }
        public int? BuyCount { get; set; }
        public int? FreeCount { get; set; }
        public int Priority { get; set; }
        public bool? MultiplierEffectEnabled { get; set; }
        public bool? UseDiscountRateAsPercentage { get; set; }
        public bool? UseDiscountRateAsToFixedAmount { get; set; }
        public decimal? DiscountRate { get; set; }
        public bool? Deleted { get; set; }
        public bool IsValidForCorporateCustomers { get; set; }
        public string CorporateDomainNames { get; set; }
        public string RoleIds { get; set; }
        public bool DisableCouponDiscountUsage { get; set; }
        public int StoreId { get; set; }
        public int MultiplierEffectDepending { get; set; }
        public bool? ApplyToDealProducts { get; set; }
        public bool InvalidForProductsWithOldPrice { get; set; }
        public string SelectedShipmentMethod { get; set; }
        public string SelectedPaymentBankNames { get; set; }
        public string SelectedPaymentCreditCartBankNames { get; set; }
        public int? CouponSaveCampaignId { get; set; }
        public bool SendNotification { get; set; }
        public string RestrictedToCustomerNthOrder { get; set; }
        public int? CampaignUsageLimitationType { get; set; }
        public int? CampaignUsageLimitationCount { get; set; }
        public string Customers { get; set; }
        public int? CampaignUsageType { get; set; }
        public string CustomDescription { get; set; }
        public string CustomTitle { get; set; }
        public bool CouponSave { get; set; }
        public int? SelectDiscountPriceType { get; set; }
        public int CampaignType { get; set; }
        public string BuyConditionIncludedProductIds { get; set; }
        public string FreeConditionIncludedProductIds { get; set; }
        public string BuyConditionManufacturers { get; set; }
        public string FreeConditionManufacturers { get; set; }
        public int MultiplierEffectLimit { get; set; }
        public string AssignedSpecOptionId { get; set; }
        public string SystemName { get; set; }
        public string BuyConditionIncludedProductSkus { get; set; }
        public string FreeConditionIncludedProductSkus { get; set; }
        public string BuyConditionExcludedProductSkus { get; set; }
        public string FreeConditionExcludedProductSkus { get; set; }
        public int BuyConditionIncludedSpecificationsCalculateTypeId { get; set; }
        public int BuyConditionExcludedSpecificationsCalculateTypeId { get; set; }
        public int FreeConditionIncludedSpecificationsCalculateTypeId { get; set; }
        public int FreeConditionExcludedSpecificationsCalculateTypeId { get; set; }
        public bool UseDiscountRateAsToFixedAmountForProduct { get; set; }
        public int CampaignCalculateTypeId { get; set; }
        public int FreeableItemOrderById { get; set; }
        public string NonMergableCampaignIds { get; set; }
        public decimal BuyConditionMaxCartTotal { get; set; }
        public int? InstallmentCount { get; set; }
        public bool NotAllowReturnOrder { get; set; }
        public string CountryIds { get; set; }
        public bool PickupInStore { get; set; }
        public string PaymentMethodSystemNames { get; set; }
        public string DeviceTypes { get; set; }
        public bool AddAutoGiftProductToBasket { get; set; }
        public decimal StaffSpendingLimit { get; set; }
        public string IncreasingDiscounts { get; set; }
        public List<int> BuyConditionCategoriesList { get; set; }
        public List<int> BuyConditionIncludedProductIdList { get; set; }
        public List<int> BuyConditionExcludedProductIdList { get; set; }

        public virtual ICollection<CampaignCouponCodeModel> CampaignServiceCampaignCouponCodes { get; set; }
        public virtual ICollection<CampaignCouponUsageModel> CampaignServiceCampaignCouponUsages { get; set; }
        public virtual ICollection<CampaignUsageHistoryModel> CampaignServiceCampaignUsageHistories { get; set; }
    }
}
