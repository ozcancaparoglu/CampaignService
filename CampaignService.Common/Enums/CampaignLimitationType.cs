namespace CampaignService.Common.Enums
{
    public enum CampaignLimitationType
	{
		/// <summary>
		/// None
		/// </summary>
		Unlimited = 0,
		/// <summary>
		/// N Times Per Customer
		/// </summary>
		NTimesPerCustomer = 10,
		/// <summary>
		/// N Times Only
		/// </summary>
		NTimesOnly = 20,
		/// <summary>
		/// N Times Per Customer Per Calendar Year
		/// </summary>
		NTimesPerCustomerPerCalendarYear = 30,
		/// <summary>
		/// N Times Per Customer Per Day
		/// </summary>
		NTimesPerCustomerPerDay = 40
	}
}
