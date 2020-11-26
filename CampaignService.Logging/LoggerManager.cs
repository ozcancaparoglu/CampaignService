using NLog;
using NLog.Web;

namespace CampaignService.Logging
{

    namespace CampaignService.Logging
    {
        public class LoggerManager : ILoggerManager
        {
            //private static ILoggerB loggera = LogManager.GetCurrentClassLogger();
            Logger logger = NLog.Web.NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();

            public void LogDebug(string message)
            {
                logger.Debug(message);
            }

            public void LogError(string message)
            {
                logger.Error(message);
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
