using CampaignService.Common.Entities;
using System;

namespace CampaignService.Data.Models
{
    public class CustomerModel : EntityBaseModel
    {
        public Guid CustomerGuid { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int PasswordFormatId { get; set; }
        public string PasswordSalt { get; set; }
        public string AdminComment { get; set; }
        public bool IsTaxExempt { get; set; }
        public int AffiliateId { get; set; }
        public int VendorId { get; set; }
        public bool HasShoppingCartItems { get; set; }
        public bool Active { get; set; }
        public bool Deleted { get; set; }
        public bool IsSystemAccount { get; set; }
        public bool IsEmailValidated { get; set; }
        public string SystemName { get; set; }
        public string LastIpAddress { get; set; }
        public DateTime CreatedOnUtc { get; set; }
        public DateTime? LastLoginDateUtc { get; set; }
        public DateTime LastActivityDateUtc { get; set; }
        public int? BillingAddressId { get; set; }
        public int? ShippingAddressId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ErpCode { get; set; }
        public string LoyaltyCardNumber { get; set; }
        public DateTime? LoyaltyCardDefinedOnUtc { get; set; }
        public string VirtualLoyaltyCardNumber { get; set; }
        public string OldWebsiteUserId { get; set; }
        public int OrderCount { get; set; }
        public int? CorporateCustomerId { get; set; }
        public string MobilePhone { get; set; }
        public bool? LoyaltyCardConfirmationCodeConfirmed { get; set; }
        public bool? LoyaltyCardIsActive { get; set; }
        public string Gender { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public bool AllowEmailCommunication { get; set; }
        public bool AllowSmsCommunication { get; set; }
        public string StreetAddress { get; set; }
        public int StateProvinceId { get; set; }
        public int CountyId { get; set; }
        public int CountryId { get; set; }
        public string ZipPostalCode { get; set; }
        public int LanguageId { get; set; }
        public string LastContinueShoppingPage { get; set; }
        public bool SelectedPickUpInStore { get; set; }
        public bool UseRewardPointsDuringCheckout { get; set; }
        public string AccountActivationToken { get; set; }
        public DateTime? CustomerInfoUpdatedOn { get; set; }
        public bool LanguageAutomaticallyDetected { get; set; }
        public int CurrencyId { get; set; }
        public bool IsPhoneValidated { get; set; }
        public string AppliedCartCampaigns { get; set; }
        public string AppliedCargoCampaigns { get; set; }
        public string AppliedLoyaltyPoints { get; set; }
    }
}
