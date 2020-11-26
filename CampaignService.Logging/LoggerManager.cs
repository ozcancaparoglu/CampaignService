using NLog;
using NLog.Web;

namespace CampaignService.Logging
{
    public class LoggerManager : ILoggerManager
    {
        private readonly Logger logger = NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();

        /// <summary>
        /// write nlog at debug log level
        /// </summary>
        /// <param name="logModelRequest">LogRequestModel</param>
        /// <returns></returns>
        public void LogDebug(LogRequestModel logModelRequest)
        {
            logger.WithProperty("EntityType", logModelRequest.EntityType)
               .WithProperty("EntityId", logModelRequest.EntityId)
               .WithProperty("ProcessBy", logModelRequest.ProcessBy)
           .Debug(logModelRequest.Message);
        }

        /// <summary>
        /// write nlog at debug log Error
        /// </summary>
        /// <param name="logModelRequest">LogRequestModel</param>
        /// <returns></returns>
        public void LogError(LogRequestModel logModelRequest)
        {
            logger.WithProperty("EntityType", logModelRequest.EntityType)
               .WithProperty("EntityId", logModelRequest.EntityId)
               .WithProperty("ProcessBy", logModelRequest.ProcessBy)
           .Error(logModelRequest.Message);
        }

        /// <summary>
        /// write nlog at debug log Info
        /// </summary>
        /// <param name="logModelRequest">LogRequestModel</param>
        /// <returns></returns>
        public void LogInfo(LogRequestModel logModelRequest)
        {
            logger.WithProperty("EntityType", logModelRequest.EntityType)
                .WithProperty("EntityId", logModelRequest.EntityId)
                .WithProperty("ProcessBy", logModelRequest.ProcessBy)
            .Info(logModelRequest.Message);
        }
    }
}
