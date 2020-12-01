using System.Collections.Generic;

namespace CampaignService.Common.Models
{
    // AccountAndSegment myDeserializedClass = JsonConvert.DeserializeObject<AccountAndSegment>(myJsonResponse); 

    public class AccountAndSegment
    {
        public List<CustomerSegment> CustomerSegments { get; set; }
        public List<AccountId> AccountIds { get; set; }
    }

    public class CustomerSegment
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }

    public class AccountId
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }
}
