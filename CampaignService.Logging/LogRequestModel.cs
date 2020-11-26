using System;
using System.Collections.Generic;
using System.Text;

namespace CampaignService.Logging
{
    public class LogRequestModel
    {
        public string EntityType { get; set; }
        public int EntityId { get; set; }
        public string Message { get; set; }
        public int ProcessBy { get; set; }
        public NLog.LogLevel LogLevel { get; set; }
    }
}
