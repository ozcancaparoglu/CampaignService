namespace CampaignService.Logging.CampaignService.Logging
{
    public interface ILoggerManager
    {
        void LogDebug(string message);
        void LogError(string message);
        void LogInfo(LogRequestModel logModelRequest);
    }
}