namespace CampaignService.Service.Model
{
    public class CampaignRequest
    {
        public int CustomerId { get; set; }
        public string CampaignCouponCode { get; set; }
        public string Email { get; set; }
        public string LastShippingOption { get; set; }
        public string BankName { get; set; }
        public string CardBankName { get; set; }
        public string PaymentMethodSystemName { get; set; }
        public int PaymentCardId { get; set; }
        public int InstallmentCount { get; set; }
        public int[] CustomerRoleIds { get; set; }
        public bool IsCustomerGuest { get; set; }
        public bool OnlyCalculateDiscount { get; set; }
        public int? CountryId { get; set; }
        public string CountryCode { get; set; }
        public bool PickupInStore { get; set; }
        public DeviceTypes DeviceType { get; set; }
        public string CurrencyCode { get; set; }
    }
}
