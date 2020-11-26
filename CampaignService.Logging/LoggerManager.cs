using NLog;
using NLog.Web;

namespace CampaignService.Logging
{

    namespace CampaignService.Logging
    {
        public class LoggerManager : ILoggerManager
        {
            Logger logger = NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();

            public void LogDebug(LogRequestModel logModelRequest)
            {
                logger.WithProperty("EntityType", logModelRequest.EntityType)
                   .WithProperty("EntityId", logModelRequest.EntityId)
                   .WithProperty("ProcessBy", logModelRequest.ProcessBy)
               .Debug(logModelRequest.Message);
            }

            public void LogError(LogRequestModel logModelRequest)
            {
                logger.WithProperty("EntityType", logModelRequest.EntityType)
                   .WithProperty("EntityId", logModelRequest.EntityId)
                   .WithProperty("ProcessBy", logModelRequest.ProcessBy)
               .Error(logModelRequest.Message);
            }

            public void LogInfo(LogRequestModel logModelRequest)
            {
                logger.WithProperty("EntityType", logModelRequest.EntityType)
                    .WithProperty("EntityId", logModelRequest.EntityId)
                    .WithProperty("ProcessBy", logModelRequest.ProcessBy)
                .Info(logModelRequest.Message);
            }
        }
    }
}
