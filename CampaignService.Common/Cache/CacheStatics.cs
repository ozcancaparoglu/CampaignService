namespace CampaignService.Common.Cache
{
    public class CacheStatics
    {
        /// <summary>
        /// All Active Campaigns
        /// </summary>
        public static string AllActiveCampaigns = "allActiveCampaigns";
        public static int AllActiveCampaignsCacheTime = 120;

        /// <summary>
        /// Campaing Filters join key with Campaign Id
        /// </summary>
        public static string CampaignFilters = "campaignFilters";
        public static int CampaignFiltersCacheTime = 120;

        /// <summary>
        /// Customer join key with Customer Id
        /// </summary>
        public static string Customer = "Customer";
        public static int CustomerCacheTime = 120;

        /// <summary>
        /// CouponCode join key with Campaign Id
        /// </summary>
        public static string CouponCode = "CouponCode";
        public static int CouponCodeCacheTime = 120;



    }
}
