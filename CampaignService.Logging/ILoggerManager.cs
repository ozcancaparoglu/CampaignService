namespace CampaignService.Logging.CampaignService.Logging
{
    public interface ILoggerManager
    {
        void LogDebug(LogRequestModel logModelRequest);
        void LogError(LogRequestModel logModelRequest);
        void LogInfo(LogRequestModel logModelRequest);
    }
}